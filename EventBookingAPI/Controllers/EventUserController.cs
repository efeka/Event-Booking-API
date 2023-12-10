using EventBookingAPI.Data;
using EventBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EventBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventUserController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        public EventUserController(IConfiguration config)
        {
            _dapper = new(config);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventUsersAsync()
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.EventUser";

            try
            {
                IEnumerable<EventUser> eventUsers = await _dapper.LoadDataAsync<EventUser>(sql);
                return Ok(eventUsers);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return NotFound("Could not retrieve event users");
            }
        }

        
    }
}
