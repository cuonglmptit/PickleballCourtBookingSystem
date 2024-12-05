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
    public class CourtPriceController : ControllerBase
    {
        private readonly ICourtPriceService _courtPriceService;
        public CourtPriceController(ICourtPriceService courtPriceService)
        {
            _courtPriceService = courtPriceService;
        }

        [HttpGet]
        public IActionResult GetAllCourtPrice()
        {
            var result = _courtPriceService.GetAllService();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCourtPriceById(Guid id)
        {
            var result = _courtPriceService.GetByIdService(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult PostCourtPrice([FromBody] CourtPrice courtPrice)
        {
            courtPrice.Id = Guid.NewGuid();
            var result = _courtPriceService.InsertService(courtPrice);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpPost("multiple")]
        public IActionResult PostManyCourtPrice([FromBody] List<CourtPrice> courtPrices)
        {
            foreach (var courtPrice in courtPrices)
            {
                courtPrice.Id = Guid.NewGuid();
            }
            var result = _courtPriceService.InsertManyService(courtPrices);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest(new { success = false, statusCode = result.StatusCode, userMessage = result.UserMsg, developerMessage = result.DevMsg });
        }

        [HttpPut]
        public IActionResult UpdateCourtPrice([FromBody] CourtPrice courtPrice, Guid id)
        {
            var result = _courtPriceService.UpdateCustomFieldService(courtPrice, id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCourtPrice(Guid id)
        {
            var result = _courtPriceService.DeleteService(id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest();
        }

    }
}
