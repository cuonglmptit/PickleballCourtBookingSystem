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
        private string connectionString =
            "User Id = umd3xmuqwg3vwhpj; Password = G1T2p4fWIUgmsKrZl8Qp; Host=bliamiwwk9iex7i9hjc8-mysql.services.clever-cloud.com; Port=3306; Character Set=utf8; Database = bliamiwwk9iex7i9hjc8";
        [HttpGet]
        public IActionResult GetAllUser()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            string query = "SELECT * FROM User;";
            var users = conn.Query<User>(sql:query);
            conn.Dispose();
            return StatusCode(200, users);
        }

    //     [HttpPost]
    //
    //     public IActionResult AddUser([FromBody] User user)
    //     {
    //         MySqlConnection conn = new MySqlConnection(connectionString);
    //         string query =
    //             "INSERT INTO USER (id, code, username, password, email, phoneNumber, isActive, addressId, fullNameId, dateOfBirth)";
    //         
    //         
    //     }
    }
}
