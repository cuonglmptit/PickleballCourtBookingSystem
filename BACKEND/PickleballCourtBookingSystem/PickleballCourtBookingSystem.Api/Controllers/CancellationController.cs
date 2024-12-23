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
    public class CancellationController : ControllerBase
    {
        private readonly ICancellationService _cancellationService;
        public CancellationController(ICancellationService cancellationService)
        {
            _cancellationService = cancellationService;
        }

        [HttpGet]
        public IActionResult GetAllCancellation()
        {
            var result = _cancellationService.GetAllService();
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCancellationById(Guid id)
        {
            var result = _cancellationService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult PostCancellation([FromBody] Cancellation cancellation)
        {
            cancellation.Id = Guid.NewGuid();
            var result = _cancellationService.InsertService(cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult UpdateCancellation([FromBody] Cancellation cancellation, Guid id)
        {
            var result = _cancellationService.UpdateService(cancellation, id);
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCancellation(Guid id)
        {
            var result = _cancellationService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
