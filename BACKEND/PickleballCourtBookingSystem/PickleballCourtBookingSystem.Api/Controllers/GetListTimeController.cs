using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Api.Controllers;

public class GetListTimeController : Controller
{
    
    private readonly IGetListTimeService _getListTimeService;

    public GetListTimeController(IGetListTimeService getListTimeService)
    {
        _getListTimeService = getListTimeService;
    }

    [HttpGet("api/GetListTime")]
    public IActionResult GetListTime()
    {
        var result = _getListTimeService.GetListTime();
        return StatusCode(result.StatusCode, result);
    }
}