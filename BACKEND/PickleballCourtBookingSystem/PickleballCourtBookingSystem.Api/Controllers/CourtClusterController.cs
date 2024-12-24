using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.DTOs;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
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
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("getCourtClusterForTimeRange")]
        public IActionResult GetCourtClusterForTime([FromBody] TimeRangeRequest timeRangeRequest)
        {
            var result = _courtClusterService.GetCourtClustersForTimeRange(timeRangeRequest.Date, timeRangeRequest.StartTime, timeRangeRequest.EndTime);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("getCourtClusterAvailableForTime")]
        public IActionResult GetAvailableCourtClusterForTime([FromBody] TimeRangeRequest timeRangeRequest)
        {
            var result = _courtClusterService.GetAvailableCourtClusterForTime(timeRangeRequest.Date, timeRangeRequest.StartTime, timeRangeRequest.EndTime);
            return StatusCode(result.StatusCode, result);
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
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Post 1 CourtCluster
        /// </summary>
        /// <param name="courtCluster"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostCourtCluster([FromBody] CourtCluster courtCluster)
        {
            courtCluster.Id = Guid.NewGuid();
            var result = _courtClusterService.InsertService(courtCluster);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("AutoCreateCourtTimeSlot")]
        public IActionResult AutoCreateCourtTimeSlot([FromBody] AutoAddCourtTimeSlotRequest request)
        {
            var result = _courtClusterService.AddTimeSlotsWithDefaultPrice(request.CourtClusterId, request.Dates);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult UpdateCourtCluster([FromBody] CourtCluster courtCluster, Guid id)
        {
            var result = _courtClusterService.UpdateService(courtCluster, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourtCluster(Guid id)
        {
            var result = _courtClusterService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}/Courts")]
        public IActionResult GetCourtsOfACourtCluster(Guid id)
        {
            var result = _courtClusterService.GetCourtsByClusterId(id);
            return StatusCode(result.StatusCode, result);
        }


        /// <summary>
        /// Post 1 list CourtCluster
        /// </summary>
        /// <param name="courtClusters"></param>
        /// <returns></returns>
        [HttpPost("dummy")]
        public IActionResult PostListCourtCluster([FromBody] List<CourtCluster> courtClusters)
        {
            Dictionary<string, ServiceResult> failedRecords = new Dictionary<string, ServiceResult>();
            foreach (var courtCluster in courtClusters)
            {
                courtCluster.Id = Guid.NewGuid();
                var result = _courtClusterService.InsertService(courtCluster);
                if (!result.Success)
                {
                    failedRecords.Add(courtCluster.Name, result);
                }
            }
            return StatusCode(201,
                new
                {
                    Result = $"insert thành công {courtClusters.Count - failedRecords.Count}/{courtClusters.Count} bản ghi",
                    Failed = failedRecords
                });
        }
    }
}
