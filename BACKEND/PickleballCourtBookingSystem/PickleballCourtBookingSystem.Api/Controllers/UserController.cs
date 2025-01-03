using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.DTOs.AuthDTOs;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.DTOs.UserDTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;
using PickleballCourtBookingSystem.Infrastructure.Repository;

namespace PickleballCourtBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var result = _userService.GetAllService();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var result = _userService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] User user)
        {
            user.Id = Guid.NewGuid();
            var result = _userService.InsertService(user);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update user với UpdateUserDTO (chỉ được update những trường trong UpdateUserDTO)
        /// </summary>
        /// <param name="user">DTO</param>
        /// <param name="id">id của user muốn update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO userDTO, Guid id)
        {
            var result = _userService.UpdateService(userDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update password cho người dùng
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("change-password/{id}")]
        public IActionResult UpdatePasswordUser([FromBody] UpdatePasswordDTO passwordDTO, Guid id)
        {
            var result = _userService.ChangePassword(passwordDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var result = _userService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("dummy")]
        public IActionResult PostListUser([FromBody] List<RegisterRequest> users)
        {
            Dictionary<string, ServiceResult> failedRecords = new Dictionary<string, ServiceResult>();
            //Write too insert the list and return the number of rows inserted
            foreach (var user in users)
            {
                var newUser = new User
                {
                    //****note quan trọng, bắt buộc phải có guid mới có thể check unique
                    // không thì nó sẽ là null và kết qua trả về sẽ sai
                    // null đại diện cho một giá trị không xác định, và bất kỳ phép so sánh nào với null,
                    // bao gồm !=, đều trả về giá trị false
                    Id = Guid.Empty,
                    Username = user.Username.Trim(),
                    Password = user.Password,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    RoleId = (int)user.Role
                };
                var result = _authService.Register(newUser.Username, newUser.Password, newUser.Password, newUser.Name, newUser.PhoneNumber, newUser.Email, (RoleEnum)newUser.RoleId);
                if (!result.Success)
                {
                    failedRecords.Add(user.Username, result); // Use Username as the key
                }
            }

            return StatusCode(201,
                new
                {
                    Result = $"insert thành công {users.Count - failedRecords.Count}/{users.Count} bản ghi",
                    Failed = failedRecords
                });
        }

        [HttpPost("dummy/Owner")]
        public IActionResult PostListOwner([FromBody] List<User> users)
        {
            Dictionary<string, ServiceResult> failedRecords = new Dictionary<string, ServiceResult>();
            //Write too insert the list and return the number of rows inserted
            foreach (var user in users)
            {
                var result = _userService.InsertService(user);
                if (!result.Success)
                {
                    failedRecords.Add(user.Username, result); // Use Username as the key
                }
            }

            return StatusCode(201,
                new
                {
                    Result = $"insert thành công {users.Count - failedRecords.Count}/{users.Count} bản ghi",
                    Failed = failedRecords
                });
        }
        
        [HttpGet("GetInfo/{id}")]
        [Authorize]
        public IActionResult GetUserInfo(Guid id)
        {
            var result = _userService.GetPublicInfoService(id);
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet("Customer/{id}/info")]
        public IActionResult GetCustomerInfo(Guid id)
        {
            var result = _userService.GetInfoByCustomerId(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
