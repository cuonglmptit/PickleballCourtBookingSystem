using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.DTOs;
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
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCourtTimeSlotById(Guid id)
        {
            var result = _courtTimeSlotService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("single")]
        public IActionResult PostCourtTimeSlot([FromBody] CourtTimeSlot courtTimeSlot)
        {
            courtTimeSlot.Id = Guid.NewGuid();
            var result = _courtTimeSlotService.InsertService(courtTimeSlot);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("multiple")]
        public IActionResult PostManyCourtTimeSlot([FromBody] List<CourtTimeSlot> courtTimeSlots)
        {
            foreach (var courtTimeSlot in courtTimeSlots)
            {
                courtTimeSlot.Id = Guid.NewGuid();
            }
            var result = _courtTimeSlotService.InsertManyService(courtTimeSlots);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult UpdateCourtTimeSlot([FromBody] CourtTimeSlot courtTimeSlot, Guid id)
        {
            var result = _courtTimeSlotService.UpdateService(courtTimeSlot, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourtTimeSlot(Guid id)
        {
            var result = _courtTimeSlotService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Court")]
        public IActionResult GetCourtTimeSlot([FromBody] GetTimeSlotDTO getTimeSlotDTO)
        {
            var result = _courtTimeSlotService.FindCourtTimeSlotsByCourtId(getTimeSlotDTO.CourtId, getTimeSlotDTO.Date);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("getCourtTimeSlot")]
        public IActionResult GetCourtTimeSlot([FromQuery] Guid courtId)
        {
            var result = _courtTimeSlotService.GetAvailableCourtTimeSlotsByCourtId(courtId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("getAvailableSlots")]
        public IActionResult GetCourtTimeSlotByDate([FromQuery] Guid courtId, [FromQuery] DateTime date)
        {
            var result = _courtTimeSlotService.GetAvailableCourtTimeSlotsByCourtIdAndDate(courtId, date);
            return StatusCode(result.StatusCode, result);
        }
    }
}
