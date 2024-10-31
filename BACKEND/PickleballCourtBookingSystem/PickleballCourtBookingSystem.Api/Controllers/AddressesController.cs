using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

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
        public IActionResult getAllAddress()
        {
            IEnumerable<Address> addresses = _repository.GetAll();
            return Ok(addresses);
        }

    }
}
