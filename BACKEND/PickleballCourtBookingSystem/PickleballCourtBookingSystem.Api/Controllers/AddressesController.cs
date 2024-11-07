using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Infrastructure.Repository;

namespace PickleballCourtBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        IBaseRepository<Address> _repository;
        public AddressesController(IBaseRepository<Address> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllAddress()
        {
            IEnumerable<Address> addresses = _repository.GetAll();
            return Ok(addresses);
        }

        [HttpPost]
        public IActionResult PostAddress([FromBody] Address address)
        {
            if (_repository.Insert(address) > 0)
            {
                return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
