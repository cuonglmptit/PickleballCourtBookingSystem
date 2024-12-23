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
    public class CourtOwnerController : ControllerBase
    {
        private readonly ICourtOwnerService _courtOwnerService;
        public CourtOwnerController(ICourtOwnerService courtOwnerService)
        {
            _courtOwnerService = courtOwnerService;
        }

        [HttpGet]
        public IActionResult GetAllCourtOwner()
        {
            var result = _courtOwnerService.GetAllService();
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCourtOwnerById(Guid id)
        {
            var result = _courtOwnerService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult PostCourtOwner([FromBody] CourtOwner courtOwner)
        {
            courtOwner.Id = Guid.NewGuid();
            var result = _courtOwnerService.InsertService(courtOwner);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult UpdateCourtOwner([FromBody] CourtOwner courtOwner, Guid id)
        {
            var result = _courtOwnerService.UpdateService(courtOwner, id);
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCourtOwner(Guid id)
        {
            var result = _courtOwnerService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
