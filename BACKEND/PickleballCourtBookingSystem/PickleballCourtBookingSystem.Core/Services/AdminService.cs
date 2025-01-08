using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;
using PickleballCourtBookingSystem.Core.PEnum;

namespace PickleballCourtBookingSystem.Core.Services;

public class AdminService : BaseService<Admin>, IAdminService
{
    //Khai báo user service
    private readonly IUserService _userService;
    //Khai báo court cluster service
    private readonly ICourtClusterService _courtClusterService;
    //Khai báo courtowner service
    private readonly ICourtOwnerService _courtOwnerService;
    //Khai báo courcluster repository
    private readonly ICourtClusterRepository _courtClusterRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly ICancellationRepository _cancellationRepository;
    public AdminService(IAdminRepository repository, IUserService userService, ICourtClusterService courtClusterService,
        ICourtOwnerService courtOwnerService, ICourtClusterRepository courtClusterRepository,
        IBookingRepository bookingRepository, ICancellationRepository cancellationRepository) : base(repository)
    {
        //Tiêm phụ thuộc
        _userService = userService;
        _courtClusterService = courtClusterService;
        _courtOwnerService = courtOwnerService;
        _courtClusterRepository = courtClusterRepository;
        _bookingRepository = bookingRepository;
        _cancellationRepository = cancellationRepository;
    }

    public ServiceResult GetListCourtClusterForAdmin(CourtClusterStatusEnum? status = null)
    {
        try
        {
            IEnumerable<CourtCluster> courtClusters;
            //Tạo điều kiện lấy court cluster
            Dictionary<string, object> conditions = new();
            //Nếu mà status = null thì lấy tất cả court cluster
            if (status != null)
            {
                conditions.Add("Status", (int)status);
                courtClusters = _courtClusterRepository.GetByMultipleConditions(conditions);
            }
            else
            {
                courtClusters = _courtClusterRepository.GetAll();
            }
            //Tạo list CourtClusterAdminDTO
            List<CourtClusterAdminDTO> courtClusterAdminDTOs = new();
            //Với mỗi court cluster thì tìm chủ sở hữu của nó
            foreach (var courtCluster in courtClusters)
            {
                //Lấy courtowner từ courtOwnerId
                var courtOwnerRes = _courtOwnerService.GetByIdService(courtCluster.CourtOwnerId.Value);
                //Check couert owner có lỗi không
                if (!courtOwnerRes.Success)
                {
                    return CreateServiceResult(Success: false, UserMsg: "Không lấy được court owner", DevMsg: "Không lấy được court owner", StatusCode: 500);
                }
                //Ép kiểu court owner
                var courtOwner = courtOwnerRes.Data as CourtOwner;
                //Lấy ra user từ courtOnwer.UserId
                var userRes = _userService.GetByIdService(courtOwner.UserId.Value);
                //Kiểm tra xem có lỗi không
                if (!userRes.Success)
                {
                    return CreateServiceResult(Success: false, UserMsg: "Không lấy được user", DevMsg: "Không lấy được user", StatusCode: 500);
                }
                //Ép kiểu user
                var userEntity = userRes.Data as User;
                //Lấy ra các court của cluster
                var courtsRes = _courtClusterService.GetCourtsByClusterId(courtCluster.Id.Value);
                //Ép thành court
                var courts = courtsRes.Data as List<Court>;
                //Gán user vào courtclusterAdminDTO
                CourtClusterAdminDTO courtClusterAdminDTO = new()
                {
                    CourtCluster = courtCluster,
                    Owner = userEntity,
                    Courts = courts
                };
                //Thêm vào list
                courtClusterAdminDTOs.Add(courtClusterAdminDTO);
            }
            //Nêu danh sach rỗng thì trả về lỗi
            if (courtClusterAdminDTOs.Count == 0)
            {
                return CreateServiceResult(Success: false, UserMsg: "Không có court cluster nào", DevMsg: "Không có court cluster nào", StatusCode: 404);
            }
            return CreateServiceResult(Success: true, UserMsg: "Lấy danh sách court cluster thành công", DevMsg: "Lấy danh sách court cluster thành công", Data: courtClusterAdminDTOs, StatusCode: 200);
        }
        catch (Exception e)
        {
            return CreateServiceResult(Success: false, UserMsg: "Lỗi không lấy được", DevMsg: e.Message, StatusCode: 500);
        }
    }

    public ServiceResult UpdateClusterStatus(Guid clusterId, CourtClusterStatusEnum status)
    {
        try
        {
            //Lấy court cluster
            var courtCluster = _courtClusterService.GetByIdService(clusterId);
            //Kiểm tra court cluster có tồn tại không
            if (!courtCluster.Success)
            {
                return CreateServiceResult(Success: false, UserMsg: "Không tìm thấy court cluster", DevMsg: "Không tìm thấy court cluster", StatusCode: 404);
            }
            //Ép kiểu court cluster để thành court cluster
            var courtClusterEntity = courtCluster.Data as CourtCluster;
            //Nếu staus là Inactive thì set thái hủy hết booking của cluster từ thời điểm ngưng hoạt động
            if(status == CourtClusterStatusEnum.Inactive)
            {
                //Lấy ra danh sách booking của cluster mà đang trong trạng thái Pending
                var bookings = _bookingRepository.GetByMultipleConditions(new Dictionary<string, object>
                {
                    { nameof(Booking.CourtClusterId), clusterId },
                    { nameof(Booking.Status), BookingStatusEnum.Pending }
                });
                //Ép kiểu booking
                var bookingList = bookings as List<Booking>;
                //Nếu danh sách booking không rỗng thì set status của booking thành cancel
                if (bookingList.Count > 0)
                {
                    foreach (var booking in bookingList)
                    {
                        booking.Status = BookingStatusEnum.Canceled;
                        _bookingRepository.Update(booking, booking.Id.Value);
                        _cancellationRepository.Insert(new Cancellation
                        {
                            Id = Guid.NewGuid(),
                            TimeCancel = DateTime.Now,
                            BookingId = booking.Id,
                            Reason = "Hủy booking do cluster bị ngưng hoạt động"
                        });
                    }
                }
            }

            //Cập nhật status
            courtClusterEntity.Status = status;
            //Lưu lại
            var res = _courtClusterService.UpdateService(courtClusterEntity, clusterId);
            if (!res.Success)
            {
                return CreateServiceResult(Success: false, UserMsg: "Không thể cập nhật court cluster", DevMsg: "Không thể cập nhật court cluster", StatusCode: 500);
            }
            return CreateServiceResult(Success: true, UserMsg: "Cập nhật court cluster thành công", DevMsg: "Cập nhật court cluster thành công", StatusCode: 200);
        }
        catch (Exception e)
        {
            return CreateServiceResult(Success: false, UserMsg: "Lỗi server", DevMsg: e.Message, StatusCode: 500);
        }
    }
}