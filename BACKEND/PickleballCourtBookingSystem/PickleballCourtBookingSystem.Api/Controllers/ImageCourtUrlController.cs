using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;
using PickleballCourtBookingSystem.Infrastructure.Repository;

namespace PickleballCourtBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCourtUrlesController : ControllerBase
    {
        private readonly IImageCourtUrlService _imageCourtUrlService;
        public ImageCourtUrlesController(IImageCourtUrlService imageCourtUrlService)
        {
            _imageCourtUrlService = imageCourtUrlService;
        }

        [HttpGet]
        public IActionResult GetAllImageCourtUrl()
        {
            var result = _imageCourtUrlService.GetAllService();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetImageCourtUrlById(Guid id)
        {
            var result = _imageCourtUrlService.GetByIdService(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult PostImageCourtUrl([FromBody] ImageCourtUrl imageCourtUrl)
        {
            imageCourtUrl.Id = Guid.NewGuid();
            var result = _imageCourtUrlService.InsertService(imageCourtUrl);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateImageCourtUrl([FromBody] ImageCourtUrl imageCourtUrl, Guid id)
        {
            var result = _imageCourtUrlService.UpdateCustomFieldService(imageCourtUrl, id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteImageCourtUrl(Guid id)
        {
            var result = _imageCourtUrlService.DeleteService(id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest();
        }

    }
}