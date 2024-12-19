using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Api.Controllers;

public class TestController : ControllerBase
{
    private readonly ITimeService _timeService;
    private readonly IAuthService _authService;

    public TestController(ITimeService timeService, IAuthService authService)
    {
        _timeService = timeService;
        _authService = authService;
    }

    [HttpGet("getCurrentTime")]
    public IActionResult GetCurrentTime()
    {
        return StatusCode(200, _timeService.GetCurrentTime());
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var result = _authService.Login(request.Username, request.Password);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest();
    }

    [HttpGet("CheckToken")]
    public IActionResult ValidateToken([FromHeader] string token)
    {
        var result = _authService.ValidateToken(token);
        if (result != null)
        {
            return Ok("Token hop le");
        }

        return BadRequest("Token khong hop le");
    }
    
}