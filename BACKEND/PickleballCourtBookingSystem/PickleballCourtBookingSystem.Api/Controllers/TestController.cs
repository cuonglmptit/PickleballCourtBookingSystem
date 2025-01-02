using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.DTOs.AuthDTOs;
using PickleballCourtBookingSystem.Core.Common;
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

    [HttpGet("TokenUserId")]
    public IActionResult GetUserIdToken()
    {
        //var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
        //Console.WriteLine(authorizationHeader);
        //var token1 = authorizationHeader["Bearer ".Length..].Trim();
        //var userId1 = _authService.GetUserIdFromToken(token1);
        //Console.WriteLine($"Token lấy cách 1 userId: {userId1}, token cách 1: {token1}");
        var token2 = CommonMethods.GetTokenFromHeader(HttpContext);
        var userId2 = _authService.GetUserIdFromToken(token2);
        Console.WriteLine($"Token lấy cách 2 userId: {userId2}, token cách 2: {token2}");
        //return Ok($"Cách 1: Token lấy cách 1 userId: {userId1}, token cách 1: {token1}\n Cách 2: Token lấy cách 2 userId: {userId2}, token cách 2: {token2}");
        return Ok($"Token lấy cách 2 userId: {userId2}, token cách 2: {token2}");
    }

}