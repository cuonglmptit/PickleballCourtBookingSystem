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
    public class RoleesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetAllRole()
        {
            var result = _roleService.GetAllService();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetRoleById(Guid id)
        {
            var result = _roleService.GetByIdService(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult PostRole([FromBody] Role role)
        {
            role.Id = Guid.NewGuid();
            var result = _roleService.InsertService(role);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateRole([FromBody] Role role, Guid id)
        {
            var result = _roleService.UpdateCustomFieldService(role, id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(Guid id)
        {
            var result = _roleService.DeleteService(id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest();
        }

    }
}
