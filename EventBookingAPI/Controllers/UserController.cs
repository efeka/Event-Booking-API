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
        public DateTime TestConnection()
        {
            return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            string sql = @"
            SELECT *
            FROM EventBookingSchema.Users";
            return _dapper.LoadData<User>(sql);
        }

    }
}
