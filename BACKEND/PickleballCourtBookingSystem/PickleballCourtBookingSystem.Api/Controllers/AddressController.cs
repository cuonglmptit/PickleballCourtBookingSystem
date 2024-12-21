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
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById(Guid id)
        {
            var result = _addressService.GetByIdService(id);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult PostAddress([FromBody] Address address)
        {
            address.Id = Guid.NewGuid();
            var result = _addressService.InsertService(address);
            if (result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateAddress([FromBody] Address address, Guid id)
        {
            var result = _addressService.UpdateService(address, id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(Guid id)
        {
            var result = _addressService.DeleteService(id);
            if (result.Success)
            {
                return Ok(result.StatusCode);
            }
            return BadRequest();
        }

        [HttpGet("search")]
        public IActionResult SearchAddress([FromQuery] string query)
        {
            var result = _addressService.SearchAddress(query);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }

    }
}
