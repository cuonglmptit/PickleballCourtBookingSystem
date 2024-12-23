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
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCourtPriceById(Guid id)
        {
            var result = _courtPriceService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult PostCourtPrice([FromBody] CourtPrice courtPrice)
        {
            courtPrice.Id = Guid.NewGuid();
            var result = _courtPriceService.InsertService(courtPrice);
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpPost("multiple")]
        public IActionResult PostManyCourtPrice([FromBody] List<CourtPrice> courtPrices)
        {
            foreach (var courtPrice in courtPrices)
            {
                courtPrice.Id = Guid.NewGuid();
            }
            var result = _courtPriceService.InsertManyService(courtPrices);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult UpdateCourtPrice([FromBody] CourtPrice courtPrice, Guid id)
        {
            var result = _courtPriceService.UpdateService(courtPrice, id);
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCourtPrice(Guid id)
        {
            var result = _courtPriceService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

    }
}
