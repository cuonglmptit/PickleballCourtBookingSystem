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
    public class CourtTimeSlotController : ControllerBase
    {
        private readonly ICourtTimeSlotService _courtTimeSlotService;
        public CourtTimeSlotController(ICourtTimeSlotService courtTimeSlotService)
        {
            _courtTimeSlotService = courtTimeSlotService;
        }

        [HttpGet]
        public IActionResult GetAllCourtTimeSlot()
        {
            var result = _courtTimeSlotService.GetAllService();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCourtTimeSlotById(Guid id)
        {
            var result = _courtTimeSlotService.GetByIdService(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost("single")]
        public IActionResult PostCourtTimeSlot([FromBody] CourtTimeSlot courtTimeSlot)
        {
            courtTimeSlot.Id = Guid.NewGuid();
            var result = _courtTimeSlotService.InsertService(courtTimeSlot);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpPost("multiple")]
        public IActionResult PostManyCourtTimeSlot([FromBody] List<CourtTimeSlot> courtTimeSlots)
        {
            foreach (var courtTimeSlot in courtTimeSlots)
            {
                courtTimeSlot.Id = Guid.NewGuid();
            }
            var result = _courtTimeSlotService.InsertManyService(courtTimeSlots);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateCourtTimeSlot([FromBody] CourtTimeSlot courtTimeSlot, Guid id)
        {
            var result = _courtTimeSlotService.UpdateService(courtTimeSlot, id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCourtTimeSlot(Guid id)
        {
            var result = _courtTimeSlotService.DeleteService(id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest();
        }
        
        [HttpGet("getCourtTimeSlot")]
        public IActionResult GetCourtTimeSlot([FromQuery] Guid courtId)
        {
            var result = _courtTimeSlotService.GetAvailableCourtTimeSlotsByCourtId(courtId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

    }
}
