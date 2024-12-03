using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;
using PickleballCourtBookingSystem.Infrastructure.Repository;

namespace PickleballCourtBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtClusteresController : ControllerBase
    {
        private readonly ICourtClusterService _courtClusterService;
        public CourtClusteresController(ICourtClusterService courtClusterService)
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
