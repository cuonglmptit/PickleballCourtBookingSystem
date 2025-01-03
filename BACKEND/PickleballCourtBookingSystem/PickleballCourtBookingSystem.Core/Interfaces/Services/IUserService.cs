using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.DTOs.UserDTOs;
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

    /// <summary>
    /// Update user với các trường trong UpdateUserDTO
    /// </summary>
    /// <param name="updateUserDTO">DTO update</param>
    /// <param name="id">id của user muốn update</param>
    /// <returns>Kết quả update</returns>
    public ServiceResult UpdateService(UpdateUserDTO updateUserDTO, Guid id);

    /// <summary>
    /// Thay đổi password cho người dùng
    /// </summary>
    /// <param name="updatePasswordDTO">Đối tượng password</param>
    /// <param name="id">Id người dùng</param>
    /// <returns>Kết quả service</returns>
    public ServiceResult ChangePassword(UpdatePasswordDTO updatePasswordDTO, Guid id);

    public ServiceResult GetPublicInfoService(Guid userId);
}