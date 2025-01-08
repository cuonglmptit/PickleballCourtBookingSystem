using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;
using PickleballCourtBookingSystem.Core.PEnum;
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

        //Api cập nhật status của court cluster
        [HttpPut("Update/CourtClusterStatus/{clusterId}/{status}")]
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        public IActionResult UpdateClusterStatus(Guid clusterId, CourtClusterStatusEnum status)
        {
            var result = _adminService.UpdateClusterStatus(clusterId, status);
            return StatusCode(result.StatusCode, result);
        }

        //Api lấy ra các cluster cho admin
        [HttpGet("Clusters/{status?}")]
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        public IActionResult GetClusters(CourtClusterStatusEnum? status = null)
        {
            var result = _adminService.GetListCourtClusterForAdmin(status);
            return StatusCode(result.StatusCode, result);
        }
    }
}
