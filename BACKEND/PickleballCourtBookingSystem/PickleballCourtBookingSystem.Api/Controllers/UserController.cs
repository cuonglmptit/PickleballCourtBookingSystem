using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using PickleballCourtBookingSystem.Api.Models;

namespace PickleballCourtBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllUser()
        {
            MySqlConnection conn = new MySqlConnection("User Id = root; Password = Duc221Baker.; Host=127.0.0.1; Port=3306; Character Set=utf8; Database = pickleballcourtbookingsystem");
            string query = "SELECT * FROM User;";
            var users = conn.Query<User>(sql:query);
            return StatusCode(200, users);
        }

    }
}
