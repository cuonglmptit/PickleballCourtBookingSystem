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
    public class BookingesController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingesController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult GetAllBooking()
        {
            var result = _bookingService.GetAllService();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetBookingById(Guid id)
        {
            var result = _bookingService.GetByIdService(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult AddBooking([FromBody] AddBookingRequest request)
        {
            var result = _bookingService.AddBooking(request.UserId, request.CourtTimeSlotsIds, request.CourtId);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpPut]
        public IActionResult UpdateBooking([FromBody] Booking booking, Guid id)
        {
            var result = _bookingService.UpdateCustomFieldService(booking, id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(Guid id)
        {
            var result = _bookingService.DeleteService(id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest();
        }

    }
}
