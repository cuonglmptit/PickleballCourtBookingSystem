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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public IActionResult GetAllFeedback()
        {
            var result = _feedbackService.GetAllService();
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetFeedbackById(Guid id)
        {
            var result = _feedbackService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult PostFeedback([FromBody] Feedback feedback)
        {
            feedback.Id = Guid.NewGuid();
            var result = _feedbackService.InsertService(feedback);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult UpdateFeedback([FromBody] Feedback feedback, Guid id)
        {
            var result = _feedbackService.UpdateService(feedback, id);
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteFeedback(Guid id)
        {
            var result = _feedbackService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
