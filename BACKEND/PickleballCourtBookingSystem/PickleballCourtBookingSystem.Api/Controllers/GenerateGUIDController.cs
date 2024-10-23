using Microsoft.AspNetCore.Mvc;

namespace PickleballCourtBookingSystem.Api.Controllers;

public class GenerateGUIDController : Controller
{
    [HttpGet("GenerateGUID")]
    public IActionResult Index()
    {
        return StatusCode(200, Guid.NewGuid());
    }
}