using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.DTOs;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Common;
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
        private readonly IAuthService _authService;
        public CourtClusterController(ICourtClusterService courtClusterService, IAuthService authService)
        {
            _courtClusterService = courtClusterService;
            _authService = authService;
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
            [FromQuery] TimeSpan? startTime, [FromQuery] TimeSpan? endTime,
            int? pageSize, int? pageIndex, string? orderByColumn, bool? DESC = false)
        {
            var result = _courtClusterService.SearchCourtClusterWithFiltersService(cityName, courtClusterName, date, startTime, endTime,
                                                    pageSize, pageIndex, orderByColumn, DESC);
            //Console.WriteLine(result);
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
        /// <param name="addCourtClusterRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = nameof(RoleEnum.CourtOwner))]
        public IActionResult AddCourtCluster([FromBody] AddCourtClusterRequest addCourtClusterRequest)
        {
            var token = CommonMethods.GetTokenFromHeader(HttpContext);
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token bi loi khong co Id");
            }
            var result = _courtClusterService.RegisterNewCourtCluster(Guid.Parse(userId), addCourtClusterRequest.Name, addCourtClusterRequest.Description, addCourtClusterRequest.OpeningTime,
                addCourtClusterRequest.ClosingTime, addCourtClusterRequest.City, addCourtClusterRequest.District, addCourtClusterRequest.Ward, addCourtClusterRequest.Street, addCourtClusterRequest.NumberOfCourts);
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

        //Viết hàm lấy ra các courtcluter của một courtowner
        [HttpGet("GetCourtClusterByCourtOwner")]
        [Authorize(Roles = nameof(RoleEnum.CourtOwner))]
        public IActionResult GetCourtClusterByCourtOwner()
        {
            var token = CommonMethods.GetTokenFromHeader(HttpContext);
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token bị lỗi, không có id");
            }
            var result = _courtClusterService.GetCourtClusterByCourtOwner(Guid.Parse(userId));
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Lấy ra các courts của một CourtCluster
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        
        [HttpGet("Owner")]
        [Authorize(Roles = nameof(RoleEnum.CourtOwner))]
        public IActionResult GetAllCourtClusterOfOwner()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authorizationHeader["Bearer ".Length..].Trim();
            var userId = _authService.GetUserIdFromToken(token);
            if (userId == null)
            {
                return BadRequest("Token bi loi khong co User Id");
            }
            var result = _courtClusterService.GetCourtClusterByOwner(Guid.Parse(userId));
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet("Active")]
        public IActionResult GetActiveCourtCluster()
        {
            var result = _courtClusterService.GetAllActiveCourtClusters();
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet("{id}/Image")]
        public IActionResult GetImageUrl(Guid id)
        {
            var result = _courtClusterService.GetImageUrl(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
