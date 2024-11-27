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
    public class CourtTimeSlotesController : ControllerBase
    {
        private readonly ICourtTimeSlotService _courtTimeSlotService;
        public CourtTimeSlotesController(ICourtTimeSlotService courtTimeSlotService)
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

        [HttpPost]
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

        [HttpPut]
        public IActionResult UpdateCourtTimeSlot([FromBody] CourtTimeSlot courtTimeSlot, Guid id)
        {
            var result = _courtTimeSlotService.UpdateCustomFieldService(courtTimeSlot, id);
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

    }
}
