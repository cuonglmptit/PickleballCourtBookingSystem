using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.DTOs.AuthDTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {

            var result = _authService.Register(request.Username, request.Password, request.ConfirmPassword,
                                                 request.FullName, request.PhoneNumber,
                                                    request.Email, request.Role);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var result = _authService.Login(request.Username, request.Password);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("CheckToken")]
        public IActionResult ValidateToken([FromHeader] string token)
        {
            var result = _authService.ValidateToken(token);
            if (result != null)
            {
                return StatusCode(200);
            }

            return StatusCode(401);
        }
    }
}
