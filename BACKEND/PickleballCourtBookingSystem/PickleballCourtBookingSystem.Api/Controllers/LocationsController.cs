using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService _locationsService;
        public LocationsController(ILocationsService locationsService)
        {
            _locationsService = locationsService;
        }
        [HttpGet("provinces")]
        public async Task<IActionResult> GetAllProvinces()
        {
            var result = await _locationsService.GetAllProvinces();
            return StatusCode(result.StatusCode, result);
        }[HttpGet("districts")]
        public async Task<IActionResult> GetAllDistricts()
        {
            var result = await _locationsService.GetAllDistricts();
            return StatusCode(result.StatusCode, result);
        }[HttpGet("wards")]
        public async Task<IActionResult> GetAllWards()
        {
            var result = await _locationsService.GetAllWards();
            return StatusCode(result.StatusCode, result);
        }
    }
}
