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
    public class CourtTimeBookingController : ControllerBase
    {
        private readonly ICourtTimeBookingService _courtTimeBookingService;
        public CourtTimeBookingController(ICourtTimeBookingService courtTimeBookingService)
        {
            _courtTimeBookingService = courtTimeBookingService;
        }

        [HttpGet]
        public IActionResult GetAllCourtTimeBooking()
        {
            var result = _courtTimeBookingService.GetAllService();
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCourtTimeBookingById(Guid id)
        {
            var result = _courtTimeBookingService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult PostCourtTimeBooking([FromBody] CourtTimeBooking courtTimeBooking)
        {
            courtTimeBooking.Id = Guid.NewGuid();
            var result = _courtTimeBookingService.InsertService(courtTimeBooking);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult UpdateCourtTimeBooking([FromBody] CourtTimeBooking courtTimeBooking, Guid id)
        {
            var result = _courtTimeBookingService.UpdateService(courtTimeBooking, id);
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCourtTimeBooking(Guid id)
        {
            var result = _courtTimeBookingService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
