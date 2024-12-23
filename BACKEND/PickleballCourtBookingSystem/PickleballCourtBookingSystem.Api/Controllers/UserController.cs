using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.DTOs.AuthDTOs;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
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
        public UserController(IUserService userService)
        {
            _userService = userService;
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

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user, Guid id)
        {
            var result = _userService.UpdateService(user, id);
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
                    Name = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    RoleId = (int)user.Role
                };
                var result = _userService.Register(newUser);
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

    }
}
