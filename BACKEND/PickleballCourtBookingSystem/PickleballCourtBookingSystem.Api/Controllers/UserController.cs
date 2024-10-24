using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Infrastructure.Repository;

namespace PickleballCourtBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        IBaseRepository<User> _userRepository;

        #region Constructors
        /// <summary>
        /// Constructor của user controller
        /// Author: CuongLM (07/08/2024)
        /// </summary>
        /// <param name="userRepository">DI tự tiêm</param>
        /// <param name="userService">DI tự tiêm</param>
        public UserController(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<User> users = _userRepository.GetAll();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (_userRepository.Insert(user) > 0)
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
