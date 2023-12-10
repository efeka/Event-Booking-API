using System.Data;
using Dapper;
using EventBookingAPI.Data;
using EventBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EventBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        public EventController(IConfiguration config)
        {
            _dapper = new(config);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventsAsync()
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Events";

            try
            {
                IEnumerable<Event> events = await _dapper.LoadDataAsync<Event>(sql);
                return Ok(events);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return NotFound("Could not retrieve events");
            }
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventByIdAsync(int eventId)
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Events
                WHERE EventId = @EventIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@EventIdParam", eventId, DbType.Int32);

            try
            {
                Event evnt = await _dapper.LoadDataSingleWithParametersAsync<Event>(sql, sqlParameters);
                return Ok(evnt);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return NotFound($"Could not find Event with ID {eventId}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEventAsync(EventToAddDto eventToAdd)
        {
            string sql = @"
                EXEC EventBookingSchema.spEvents_Insert
                    @OrganizerId = @OrganizerIdParam,
                    @Title = @TitleParam,
                    @Description = @DescriptionParam,
                    @StartDate = @StartDateParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@OrganizerIdParam", eventToAdd.OrganizerId, DbType.Int32);
            sqlParameters.Add("@TitleParam", eventToAdd.Title, DbType.String);
            sqlParameters.Add("@DescriptionParam", eventToAdd.Description, DbType.String);
            sqlParameters.Add("@StartDateParam", eventToAdd.StartDate, DbType.DateTime);

            try
            {
                if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
                    return Ok();
                else
                    return BadRequest("Failed to insert event");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return BadRequest("Failed to insert event");
            }
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEventAsync(EventToAddDto eventToAdd, int eventId)
        {
            string sql = @"
                EXEC EventBookingSchema.spEvents_Update
                    @EventId = @EventIdParam,
                    @OrganizerId = @OrganizerIdParam,
                    @Title = @TitleParam,
                    @Description = @DescriptionParam,
                    @StartDate = @StartDateParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@EventIdParam", eventId, DbType.Int32);
            sqlParameters.Add("@OrganizerIdParam", eventToAdd.OrganizerId, DbType.Int32);
            sqlParameters.Add("@TitleParam", eventToAdd.Title, DbType.String);
            sqlParameters.Add("@DescriptionParam", eventToAdd.Description, DbType.String);
            sqlParameters.Add("@StartDateParam", eventToAdd.StartDate, DbType.DateTime);

            try
            {
                if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
                    return Ok();
                else
                    return BadRequest($"Failed to update event with ID {eventId}");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return BadRequest($"Failed to update event with ID {eventId}");
            }
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventAsync(int eventId) {
            string sql = @"
                DELETE FROM EventBookingSchema.Events
                WHERE EventId = @EventIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@EventIdParam", eventId, DbType.Int32);

            try
            {
                if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
                    return Ok();
                else
                    return BadRequest($"Failed to delete event with ID {eventId}");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return BadRequest($"Failed to delete event with ID {eventId}");
            }
        }

    }
}
