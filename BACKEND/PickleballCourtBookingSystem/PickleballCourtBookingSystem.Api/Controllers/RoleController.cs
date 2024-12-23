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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetAllRole()
        {
            var result = _roleService.GetAllService();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        
        public IActionResult GetRoleById(Guid id)
        {
            var result = _roleService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }
        
    }
}
