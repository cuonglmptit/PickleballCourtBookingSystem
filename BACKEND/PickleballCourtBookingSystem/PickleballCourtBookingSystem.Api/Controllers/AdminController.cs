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
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult GetAllAdmin()
        {
            var result = _adminService.GetAllService();
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetAdminById(Guid id)
        {
            var result = _adminService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult PostAdmin([FromBody] Admin admin)
        {
            admin.Id = Guid.NewGuid();
            var result = _adminService.InsertService(admin);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult UpdateAdmin([FromBody] Admin admin, Guid id)
        {
            var result = _adminService.UpdateService(admin, id);
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(Guid id)
        {
            var result = _adminService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
