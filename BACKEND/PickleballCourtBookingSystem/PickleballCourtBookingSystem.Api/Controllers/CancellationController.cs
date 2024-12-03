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
    public class CancellationesController : ControllerBase
    {
        private readonly ICancellationService _cancellationService;
        public CancellationesController(ICancellationService cancellationService)
        {
            _cancellationService = cancellationService;
        }

        [HttpGet]
        public IActionResult GetAllCancellation()
        {
            var result = _cancellationService.GetAllService();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCancellationById(Guid id)
        {
            var result = _cancellationService.GetByIdService(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult PostCancellation([FromBody] Cancellation cancellation)
        {
            cancellation.Id = Guid.NewGuid();
            var result = _cancellationService.InsertService(cancellation);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateCancellation([FromBody] Cancellation cancellation, Guid id)
        {
            var result = _cancellationService.UpdateCustomFieldService(cancellation, id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCancellation(Guid id)
        {
            var result = _cancellationService.DeleteService(id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest();
        }

    }
}
