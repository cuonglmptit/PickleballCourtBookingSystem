using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using PickleballCourtBookingSystem.Api.DTOs;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Common;
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
            var token = authorizationHeader["Bearer ".Length..].Trim();
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token bi loi khong co Id");
            }
            var result = _bookingService.AddBooking(Guid.Parse(userId), request.CourtTimeSlotsIds, request.CourtId);
            return StatusCode(result.StatusCode, result);
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

        /// <summary>
        /// Api cho chủ sân xác nhận đã nhận tiền từ khách hàng
        /// </summary>
        /// <param name="confirmBookingRequest"></param>
        /// <returns></returns>
        [HttpPost("court-owner-confirm-paid")]
        [Authorize(Roles = nameof(RoleEnum.CourtOwner))]
        public IActionResult CourtOwnerConfirmPaid([FromBody] ConfirmBookingRequest confirmBookingRequest)
        {
            var token = CommonMethods.GetTokenFromHeader(HttpContext);
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token không hợp lệ, không tìm thấy Id người dùng.");
            }

            var result = _bookingService.CourtOwnerConfirmPaid(Guid.Parse(userId), confirmBookingRequest.BookingId);
            return StatusCode(result.StatusCode, result);
        }

        //[HttpPost("customer-confirm-booking")]
        //[Authorize(Roles = nameof(RoleEnum.Customer))]
        //public IActionResult CustomerConfirmBooking([FromBody] ConfirmBookingRequest confirmBookingRequest)
        //{
        //    var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
        //    var token = authorizationHeader["Bearer ".Length..].Trim();
        //    var userId = _authService.GetUserIdFromToken(token);
        //    if (userId == null)
        //    {
        //        return BadRequest("Token không hợp lệ, không tìm thấy Id người dùng.");
        //    }

        //    var result = _bookingService.CustomerConfirmBooking(Guid.Parse(userId), confirmBookingRequest.BookingId);
        //    return StatusCode(result.StatusCode, result);
        //}


        [HttpPost("cancel-booking")]
        [Authorize(Roles = $"{nameof(RoleEnum.Customer)},{nameof(RoleEnum.CourtOwner)}")]
        public IActionResult CancelBooking([FromBody] CancelBookingRequest request)
        {
            var token = CommonMethods.GetTokenFromHeader(HttpContext);
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token không hợp lệ, không tìm thấy Id người dùng.");
            }
            var result = _bookingService.CancelBooking(Guid.Parse(userId), request.BookingId, request.Reason);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Customer)},{nameof(RoleEnum.CourtOwner)}")]
        [HttpGet("Status/{status}")]
        public IActionResult GetBookingByStatus(BookingStatusEnum status)
        {
            var token = CommonMethods.GetTokenFromHeader(HttpContext);
            string userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token không hợp lệ, không tìm thấy Id người dùng.");
            }
            else
            {
                var result = _bookingService.GetBookingByStatusService(Guid.Parse(userId), status);
                return StatusCode(result.StatusCode, result);
            }
        }

        [HttpGet("{bookingId}/CourTimeSlots")]
        public IActionResult GetCourtTimeBookingByBookingId(Guid bookingId)
        {
            var result = _bookingService.GetCourtTimeSlotBookingIdService(bookingId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("GetBookingSchedule")]
        public IActionResult GetAllBookingOfUser()
        {
            var token = CommonMethods.GetTokenFromHeader(HttpContext);
            string userId = _authService.GetUserIdFromToken(token);
            RoleEnum role = _authService.GetUserRoleFromToken(token).Value;

            if (userId == null)
            {
                return BadRequest("Token không hợp lệ, không tìm thấy Id người dùng.");
            }
            var result = _bookingService.GetAllBookingOfUser(Guid.Parse(userId), role);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("Statistic")]
        [Authorize(Roles = nameof(RoleEnum.CourtOwner))]
        public IActionResult GetStatistic([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var token = CommonMethods.GetTokenFromHeader(HttpContext);
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token không hợp lệ, không tìm thấy Id người dùng.");
            }
            var result = _bookingService.GetStatistic(Guid.Parse(userId), startDate, endDate);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("Statistic/All")]
        [Authorize(Roles = nameof(RoleEnum.CourtOwner))]
        public IActionResult GetStatisticAll()
        {
            var token = CommonMethods.GetTokenFromHeader(HttpContext);
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token không hợp lệ, không tìm thấy Id người dùng.");
            }
            var result = _bookingService.GetStatisticAll(Guid.Parse(userId));
            return StatusCode(result.StatusCode, result);
        }
    }
}
