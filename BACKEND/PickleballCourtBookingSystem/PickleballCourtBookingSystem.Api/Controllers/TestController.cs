using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Api.Controllers;

public class TestController : ControllerBase
{
    private readonly ITimeService _timeService;

    public TestController(ITimeService timeService)
    {
        _timeService = timeService;
    }

    [HttpGet("getCurrentTime")]
    public IActionResult GetCurrentTime()
    {
        return StatusCode(200, _timeService.GetCurrentTime());
    }

    
}