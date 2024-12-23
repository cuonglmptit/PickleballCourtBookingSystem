using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IUserService : IBaseService<User>
{
    /// <summary>
    /// Đăng ký một user mới
    /// </summary>
    /// <param name="user">User muốn đăng ký</param>
    /// <param name="errorData">Danh sách lỗi (để append thêm vào)</param>
    /// <returns>Kết quả đăng ký</returns>
    public ServiceResult Register(User user, Dictionary<string, string>? errorData = null);
}