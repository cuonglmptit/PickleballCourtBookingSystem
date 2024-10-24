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
            MySqlConnection conn = new MySqlConnection("User Id = umd3xmuqwg3vwhpj; Password = G1T2p4fWIUgmsKrZl8Qp; Host=bliamiwwk9iex7i9hjc8-mysql.services.clever-cloud.com; Port=3306; Character Set=utf8; Database = bliamiwwk9iex7i9hjc8");
            string query = "SELECT * FROM User;";
            var users = conn.Query<User>(sql:query);
            return StatusCode(200, users);
        }

    }
}
