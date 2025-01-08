using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.PEnum;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IAdminService : IBaseService<Admin>
{
    //Tạo service để update status courtcluster
    ServiceResult UpdateClusterStatus(Guid id, CourtClusterStatusEnum status);
    //Tạo service để lấy tất cả các court cluster cho admin của hệ thống
    /// <summary>
    /// Nếu truyền vào status = null thì lấy tất cả các court cluster
    /// </summary>
    /// <returns></returns>
    ServiceResult GetListCourtClusterForAdmin(CourtClusterStatusEnum? status = null);
}