using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtsController : ControllerBase
    {
        ICourtRepository _courtRepository;
        public CourtsController(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }

        [HttpGet]
        public IActionResult GetAllCourt()
        {
            var rs = _courtRepository.GetAll();
            return Ok(rs);
        }
    }
}
