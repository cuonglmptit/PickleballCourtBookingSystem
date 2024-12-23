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
    public class ImageCourtUrlController : ControllerBase
    {
        private readonly IImageCourtUrlService _imageCourtUrlService;
        public ImageCourtUrlController(IImageCourtUrlService imageCourtUrlService)
        {
            _imageCourtUrlService = imageCourtUrlService;
        }

        [HttpGet]
        public IActionResult GetAllImageCourtUrl()
        {
            var result = _imageCourtUrlService.GetAllService();
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetImageCourtUrlById(Guid id)
        {
            var result = _imageCourtUrlService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult PostImageCourtUrl([FromBody] ImageCourtUrl imageCourtUrl)
        {
            imageCourtUrl.Id = Guid.NewGuid();
            var result = _imageCourtUrlService.InsertService(imageCourtUrl);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult UpdateImageCourtUrl([FromBody] ImageCourtUrl imageCourtUrl, Guid id)
        {
            var result = _imageCourtUrlService.UpdateService(imageCourtUrl, id);
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteImageCourtUrl(Guid id)
        {
            var result = _imageCourtUrlService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
