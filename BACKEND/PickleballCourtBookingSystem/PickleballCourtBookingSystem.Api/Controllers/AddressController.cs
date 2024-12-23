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
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public IActionResult GetAllAddress()
        {
            var result = _addressService.GetAllService();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById(Guid id)
        {
            var result = _addressService.GetByIdService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public IActionResult PostAddress([FromBody] Address address)
        {
            address.Id = Guid.NewGuid();
            var result = _addressService.InsertService(address);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public IActionResult UpdateAddress([FromBody] Address address, Guid id)
        {
            var result = _addressService.UpdateService(address, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(Guid id)
        {
            var result = _addressService.DeleteService(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("search")]
        public IActionResult SearchAddress([FromQuery] string query)
        {
            var result = _addressService.SearchAddress(query);
            return StatusCode(result.StatusCode, result);
        }

    }
}
