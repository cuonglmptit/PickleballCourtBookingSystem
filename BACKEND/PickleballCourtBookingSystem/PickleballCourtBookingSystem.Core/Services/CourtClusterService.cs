using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;
using PickleballCourtBookingSystem.Core.PEnum;

namespace PickleballCourtBookingSystem.Core.Services

{
    public class CourtClusterService : BaseService<CourtCluster>, ICourtClusterService
    {
        private readonly ICourtClusterRepository _courtClusterRepository;
        private readonly ICourtService _courtService;
        private readonly ICourtPriceService _courtPriceService;
        private readonly ICourtTimeSlotService _courtTimeSlotService;
        private readonly IAddressService _addressService;
        private readonly ICourtOwnerService _courtOwnerService;
        public CourtClusterService(ICourtClusterRepository repository, ICourtClusterRepository courtClusterRepository, ICourtService courtService, ICourtPriceService courtPriceService, ICourtTimeSlotService courtTimeSlotService, IAddressService addressService, ICourtOwnerService courtOwnerService) : base(repository)
        {
            _courtClusterRepository = courtClusterRepository;
            _courtService = courtService;
            _courtPriceService = courtPriceService;
            _courtTimeSlotService = courtTimeSlotService;
            _addressService = addressService;
            _courtOwnerService = courtOwnerService;
        }

        public ServiceResult RegisterNewCourtCluster(Guid userId, string name, string? description, TimeSpan openingTime,
            TimeSpan closingTime, string city, string district, string ward, string street, int numberOfCourts)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return CreateServiceResult(false, StatusCode: 400, UserMsg: "Request error",
                        DevMsg: "Request error");
                }

                var resultCourtOwner = _courtOwnerService.GetCourtOwnerByUserIdService(userId);
                if (!resultCourtOwner.Success)
                {
                    return CreateServiceResult(false, StatusCode: 403, UserMsg: "Ban khong phai la chu san",
                        DevMsg: "Ban khong phai la chu san");
                }

                var address = new Address
                {
                    Id = Guid.NewGuid(),
                    City = city,
                    District = district,
                    Ward = ward,
                    Street = street
                };
                var resultAddAddress = _addressService.InsertService(address);
                if (!resultAddAddress.Success)
                {
                    return resultAddAddress;
                }

                var courtOwner = (CourtOwner)resultCourtOwner.Data!;
                var courtCluster = new CourtCluster
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description,
                    OpeningTime = openingTime,
                    ClosingTime = closingTime,
                    AddressId = address.Id,
                    CourtOwnerId = courtOwner.Id,
                    Status = (int)CourtClusterStatusEnum.PendingApproval
                };

                var result = _courtClusterRepository.Insert(courtCluster);
                if (result == 0)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Loi khi them court cluster");
                }

                for (int i = 1; i <= numberOfCourts; i++)
                {
                    var court = new Court
                    {
                        Id = Guid.NewGuid(),
                        CourtNumber = i,
                        CourtClusterId = courtCluster.Id,
                    };
                    var resultAddCourt = _courtService.InsertService(court);
                    if (!resultAddCourt.Success)
                    {
                        return resultAddCourt;
                    }
                }
                return CreateServiceResult(Success: true, StatusCode: 201, UserMsg: "Them cum san moi thanh cong",
                    DevMsg: "Them san thanh cong");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }

        public ServiceResult GetCourtClustersForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                var resultGetCourt = _courtService.GetCourtForTimeRange(date, startTime, endTime);
                if (!resultGetCourt.Success || resultGetCourt.Data == null)
                {
                    return resultGetCourt;
                }
                var listCourt = (List<Court>)_courtService.GetCourtForTimeRange(date, startTime, endTime).Data!;
                var listCourtCluster = new List<CourtCluster>();
                var courtClusterIds = new HashSet<Guid>();
                foreach (var court in listCourt)
                {
                    if (court.CourtClusterId == null)
                    {
                        return CreateServiceResult(false, StatusCode: 500, UserMsg: "id bi null", DevMsg: "court cluster id trong bang court la null");
                    }

                    courtClusterIds.Add(court.CourtClusterId.Value);
                }
                foreach (var courtClusterId in courtClusterIds)
                {
                    var courtCluster = _courtClusterRepository.GetById(courtClusterId);

                    if (courtCluster == null)
                    {
                        return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Court cluster not found",
                            DevMsg: "Court cluster not found");
                    }
                    listCourtCluster.Add(courtCluster);
                }
                return CreateServiceResult(Success: true, StatusCode: 200, Data: listCourtCluster);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }

        public ServiceResult GetAvailableCourtClusterForTime(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                var resultGetCourt = _courtService.GetCourtForTimeRange(date, startTime, endTime);
                if (!resultGetCourt.Success || resultGetCourt.Data == null)
                {
                    return resultGetCourt;
                }
                var listCourt = (List<Court>)_courtService.GetAvailableCourtsForTime(date, startTime, endTime).Data!;
                var listCourtCluster = new List<CourtCluster>();
                var courtClusterIds = new HashSet<Guid>();
                foreach (var court in listCourt)
                {
                    if (court.CourtClusterId == null)
                    {
                        return CreateServiceResult(false, StatusCode: 500, UserMsg: "id bi null", DevMsg: "court cluster id trong bang court la null");
                    }

                    courtClusterIds.Add(court.CourtClusterId.Value);
                }
                foreach (var courtClusterId in courtClusterIds)
                {
                    var courtCluster = _courtClusterRepository.GetById(courtClusterId);

                    if (courtCluster == null)
                    {
                        return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Court cluster not found",
                            DevMsg: "Court cluster not found");
                    }
                    listCourtCluster.Add(courtCluster);
                }
                return CreateServiceResult(Success: true, StatusCode: 200, Data: listCourtCluster);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }

        public ServiceResult SearchCourtClusterWithFiltersService(string? cityName, string? courtClusterName,
            DateTime? date, TimeSpan? startTime, TimeSpan? endTime,
            int? pageSize, int? pageIndex, string? orderByColumn, bool? DESC = false)
        {
            try
            {
                var timeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
                var dateCheck = date ?? timeNow.Date;
                //Nếu ngày tìm lớn hơn ngày hôm nay thì thời gian bắt đầu sẽ là 00:00
                //, nếu ngày tìm mà là hôm nay thì thời gian bắt đầu sẽ là hiện tại, 
                //Console.WriteLine("startTime truyen vao: " + startTime);
                var startTimeCheck = startTime ?? TimeSpan.Zero;
                if (dateCheck.Date == timeNow.Date && !startTime.HasValue)
                {
                    startTimeCheck = timeNow.TimeOfDay;
                }
                var endTimeCheck = endTime ?? DateTime.Today.AddDays(1).AddTicks(-1).TimeOfDay;
                //Bắt đầu kiểm tra
                if (dateCheck < timeNow.Date)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Ngày truyền vào trong quá khứ", DevMsg: "Ngày truyền vào trong quá khứ");
                }
                if (timeNow.Date == dateCheck && (startTimeCheck < timeNow.TimeOfDay || endTimeCheck < timeNow.TimeOfDay))
                {
                    Console.WriteLine(timeNow.Date == dateCheck);
                    Console.WriteLine(startTimeCheck < timeNow.TimeOfDay);
                    Console.WriteLine(endTimeCheck < timeNow.TimeOfDay);
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Thời gian bắt đầu hoặc kết thúc truyền vào trong quá khứ", DevMsg: "Thời gian bắt đầu hoặc kết thúc truyền vào trong quá khứ");
                }
                if (startTimeCheck > endTimeCheck)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Thời gian kết thúc lớn hơn thời gian bắt đầu", DevMsg: "Thời gian kết thúc lớn hơn thời gian bắt đầu");
                }
                if (dateCheck.Date < timeNow.Date)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Ngày truyền vào trong quá khứ", DevMsg: "Ngày truyền vào trong quá khứ");
                }

                // Xử lý phân trang: Nếu chỉ truyền một giá trị hoặc cả hai giá trị null thì bỏ qua phân trang
                if (pageSize.HasValue != pageIndex.HasValue)
                {
                    return CreateServiceResult(false, 400, "Thiếu thông tin phân trang", "pageSize và pageIndex cần được truyền cùng nhau");
                }

                // Gán giá trị mặc định nếu cần
                var pageSizeValue = pageSize ?? 0; // Nếu không phân trang, `pageSize` là 0
                var pageIndexValue = pageIndex ?? 0; // Nếu không phân trang, `pageIndex` là 0

                // Gọi đến repository
                var result = (List<CourtCluster>)_courtClusterRepository.SearchCourtClusterWithFilters(
                    cityName, courtClusterName, dateCheck, startTimeCheck, endTimeCheck,
                    pageSizeValue, pageIndexValue, orderByColumn, DESC ?? false);
                Console.WriteLine("service SearchCourtClusterWithFilters kết quả: " + result.Count);

                if (result.Count == 0)
                {
                    return CreateServiceResult(Success: false, StatusCode: 200, UserMsg: "Không tìm thấy kết quả", DevMsg: "Không tìm thấy kết quả");
                }
                return CreateServiceResult(Success: true, StatusCode: 200, Data: result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }

        public ServiceResult AddTimeSlotsWithDefaultPrice(Guid courtClusterId, List<DateTime> dates)
        {
            try
            {
                if (dates.Count == 0)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Danh sach ngay khong duoc de trong", DevMsg: "Danh sach ngay khong duoc de trong");
                }
                var resultListCourtPrice = _courtPriceService.GetCourtPricesByCourtClusterId(courtClusterId);
                if (!resultListCourtPrice.Success)
                {
                    return resultListCourtPrice;
                }

                var resultListCourt = _courtService.GetCourtsByCourtClusterId(courtClusterId);
                if (!resultListCourt.Success)
                {
                    return resultListCourt;
                }

                if (resultListCourt.Data == null)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400,
                        UserMsg: "Cum san khong co san nao", DevMsg: "Cum san khong co san nao");
                }
                var listCourtPrice = (IEnumerable<CourtPrice>)resultListCourtPrice.Data!;
                var listCourt = (IEnumerable<Court>)resultListCourt.Data!;
                var courtIds = new List<Guid>();
                foreach (var court in listCourt)
                {
                    if (!court.Id.HasValue) return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: "courtClusterId trong court la null");
                    courtIds.Add(court.Id.Value);
                }
                var result = _courtTimeSlotService.AddCourtTimeSlotWithDefaultPrice(courtIds, dates, listCourtPrice);
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }

        public ServiceResult GetCourtsByClusterId(Guid courtClusterId)
        {
            try
            {
                var result = _courtService.GetCourtsByCourtClusterId(courtClusterId);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }

        public ServiceResult GetCourtClusterByOwner(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return CreateServiceResult(false, StatusCode: 400, UserMsg: "Request error",
                        DevMsg: "Request error");
                }

                var resultCourtOwner = _courtOwnerService.GetCourtOwnerByUserIdService(userId);
                if (!resultCourtOwner.Success)
                {
                    return CreateServiceResult(false, StatusCode: 403, UserMsg: "Ban khong phai la chu san",
                        DevMsg: "Ban khong phai la chu san");
                }
                var courtOwner = (CourtOwner) resultCourtOwner.Data!;
                var result = _courtClusterRepository.FindByColumnValue(courtOwner.Id, "courtOwnerId");
                return CreateServiceResult(Success: true, StatusCode: 200, Data: result, UserMsg: "Lay thanh cong", DevMsg: "Lay thanh cong");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }
    }
}