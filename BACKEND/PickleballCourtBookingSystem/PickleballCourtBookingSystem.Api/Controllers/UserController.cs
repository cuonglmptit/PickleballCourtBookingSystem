using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.Models;
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

    }
}
