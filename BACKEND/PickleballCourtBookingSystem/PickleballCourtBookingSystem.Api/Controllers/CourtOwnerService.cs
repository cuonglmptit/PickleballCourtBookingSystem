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
    public class CourtOwneresController : ControllerBase
    {
        private readonly ICourtOwnerService _courtOwnerService;
        public CourtOwneresController(ICourtOwnerService courtOwnerService)
        {
            _courtOwnerService = courtOwnerService;
        }

        [HttpGet]
        public IActionResult GetAllCourtOwner()
        {
            var result = _courtOwnerService.GetAllService();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCourtOwnerById(Guid id)
        {
            var result = _courtOwnerService.GetByIdService(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult PostCourtOwner([FromBody] CourtOwner courtOwner)
        {
            courtOwner.Id = Guid.NewGuid();
            var result = _courtOwnerService.InsertService(courtOwner);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateCourtOwner([FromBody] CourtOwner courtOwner, Guid id)
        {
            var result = _courtOwnerService.UpdateCustomFieldService(courtOwner, id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCourtOwner(Guid id)
        {
            var result = _courtOwnerService.DeleteService(id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest();
        }

    }
}
