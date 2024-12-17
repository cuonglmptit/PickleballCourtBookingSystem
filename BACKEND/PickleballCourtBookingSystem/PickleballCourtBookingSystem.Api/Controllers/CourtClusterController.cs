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
    public class CourtClusterController : ControllerBase
    {
        private readonly ICourtClusterService _courtClusterService;
        public CourtClusterController(ICourtClusterService courtClusterService)
        {
            _courtClusterService = courtClusterService;
        }

        [HttpGet]
        public IActionResult GetAllCourtCluster()
        {
            var result = _courtClusterService.GetAllService();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        [HttpGet("getCourtClusterForTimeRange")]
        public IActionResult GetCourtClusterForTime([FromBody] TimeRangeRequest timeRangeRequest)
        {
            var result = _courtClusterService.GetCourtClustersForTimeRange(timeRangeRequest.Date, timeRangeRequest.StartTime, timeRangeRequest.EndTime);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(new { success = false, statusCode = result.StatusCode, userMessage = result.UserMsg, developerMessage = result.DevMsg });
        }

        [HttpGet("getCourtClusterAvailableForTime")]
        public IActionResult GetAvailableCourtClusterForTime([FromBody] TimeRangeRequest timeRangeRequest)
        {
            var result = _courtClusterService.GetAvailableCourtClusterForTime(timeRangeRequest.Date, timeRangeRequest.StartTime, timeRangeRequest.EndTime);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(new { success = false, statusCode = result.StatusCode, userMessage = result.UserMsg, developerMessage = result.DevMsg });
        }

        [HttpGet("SearchCourtClusterWithFilter")]
        public IActionResult SearchCourtClusterWithFiler([FromQuery] string? cityName, [FromQuery] string? courtClusterName, [FromQuery] DateTime? date,
            [FromQuery] TimeSpan? startTime, [FromQuery] TimeSpan? endTime)
        {
            var result = _courtClusterService.SearchCourtClusterWithFilters(cityName, courtClusterName, date, startTime, endTime);
            Console.WriteLine(result);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCourtClusterById(Guid id)
        {
            var result = _courtClusterService.GetByIdService(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult PostCourtCluster([FromBody] CourtCluster courtCluster)
        {
            courtCluster.Id = Guid.NewGuid();
            var result = _courtClusterService.InsertService(courtCluster);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }

        [HttpPost("AutoCreateCourtTimeSlot")]
        public IActionResult AutoCreateCourtTimeSlot([FromBody] AutoAddCourtTimeSlotRequest request)
        {
            var result = _courtClusterService.AddTimeSlotsWithDefaultPrice(request.CourtClusterId, request.Dates);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest(new { success = false, statusCode = result.StatusCode, userMessage = result.UserMsg, developerMessage = result.DevMsg });
        }

        [HttpPut]
        public IActionResult UpdateCourtCluster([FromBody] CourtCluster courtCluster, Guid id)
        {
            var result = _courtClusterService.UpdateCustomFieldService(courtCluster, id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourtCluster(Guid id)
        {
            var result = _courtClusterService.DeleteService(id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest();
        }

    }
}
