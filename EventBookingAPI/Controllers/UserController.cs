using EventBookingAPI.Data;
using EventBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        public UserController(IConfiguration config)
        {
            _dapper = new(config);
        }

        [HttpGet("TestConnection")]
        public async Task<DateTime> TestConnectionAsync()
        {
            return await _dapper.LoadDataSingleAsync<DateTime>("SELECT GETDATE()");
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Users";
            return await _dapper.LoadDataAsync<User>(sql);
        }

    }
}
