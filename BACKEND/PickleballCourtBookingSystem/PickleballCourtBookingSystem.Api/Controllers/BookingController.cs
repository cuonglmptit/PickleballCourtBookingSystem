using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using PickleballCourtBookingSystem.Api.DTOs;
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
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IAuthService _authService;
        public BookingController(IBookingService bookingService, IAuthService authService)
        {
            _bookingService = bookingService;
            _authService = authService;
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

        [HttpPost("add-booking")]
        [Authorize(Roles = nameof(RoleEnum.Customer))]
        public IActionResult AddBooking([FromBody] AddBookingRequest request)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            Console.WriteLine(authorizationHeader);
            var token = authorizationHeader["Bearer ".Length..].Trim();
            Console.WriteLine(token);
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token bi loi khong co Id");
            }
            var result = _bookingService.AddBooking(Guid.Parse(userId), request.CourtTimeSlotsIds, request.CourtId);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest(new { success = false, statusCode = result.StatusCode, userMessage = result.UserMsg, developerMessage = result.DevMsg });
        }

        [HttpPost("court-owner-confirm-booking")]
        [Authorize(Roles = nameof(RoleEnum.CourtOwner))]
        public IActionResult CourtOwnerConfirmBooking([FromBody] ConfirmBookingRequest confirmBookingRequest)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authorizationHeader["Bearer ".Length..].Trim();
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token không hợp lệ, không tìm thấy Id người dùng.");
            }

            var result = _bookingService.CourtOwnerConfirmBooking(Guid.Parse(userId), confirmBookingRequest.BookingId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("customer-confirm-booking")]
        [Authorize(Roles = nameof(RoleEnum.Customer))]
        public IActionResult CustomerConfirmBooking([FromBody] ConfirmBookingRequest confirmBookingRequest)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authorizationHeader["Bearer ".Length..].Trim();
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token không hợp lệ, không tìm thấy Id người dùng.");
            }

            var result = _bookingService.CustomerConfirmBooking(Guid.Parse(userId), confirmBookingRequest.BookingId);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost("cancel-booking")]
        [Authorize(Roles = nameof(RoleEnum.Customer))]
        public IActionResult CancelBooking([FromBody] CancelBookingRequest request)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authorizationHeader["Bearer ".Length..].Trim();
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token bi loi khong co Id");
            }
            var result = _bookingService.CancelBooking(Guid.Parse(userId), request.BookingId, request.Reason);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest(new { success = false, statusCode = result.StatusCode, userMessage = result.UserMsg, developerMessage = result.DevMsg });
        }

        //[Authorize(Roles = $"{nameof(RoleEnum.Customer)},{nameof(RoleEnum.CourtOwner)}")]
        [HttpGet("/status/{status}")]
        public IActionResult GetBookingByStatus(BookingStatusEnum status, Guid userId)
        {
            var result = _bookingService.GetBookingByStatusService(userId, status);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

    }
}
