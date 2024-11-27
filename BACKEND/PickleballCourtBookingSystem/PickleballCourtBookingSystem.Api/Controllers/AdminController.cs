using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;
using PickleballCourtBookingSystem.Infrastructure.Repository;

namespace PickleballCourtBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminesController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminesController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult GetAllAdmin()
        {
            var result = _adminService.GetAllService();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetAdminById(Guid id)
        {
            var result = _adminService.GetByIdService(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult PostAdmin([FromBody] Admin admin)
        {
            admin.Id = Guid.NewGuid();
            var result = _adminService.InsertService(admin);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateAdmin([FromBody] Admin admin, Guid id)
        {
            var result = _adminService.UpdateCustomFieldService(admin, id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(Guid id)
        {
            var result = _adminService.DeleteService(id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest();
        }

    }
}
