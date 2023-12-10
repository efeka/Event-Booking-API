using System.Data;
using Dapper;
using EventBookingAPI.Data;
using EventBookingAPI.Models;
using Microsoft.Data.SqlClient;

namespace EventBookingAPI.Services
{
    public class EventService : IEventService
    {
        private readonly DataContextDapper _dapper;
        private readonly ILogger<EventService> _logger;

        public EventService(IConfiguration config, ILogger<EventService> logger)
        {
            _dapper = new(config);
            _logger = logger;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Events";

            try
            {
                IEnumerable<Event> events = await _dapper.LoadDataAsync<Event>(sql);
                return events;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while getting events.");
                throw new ApplicationException("An error occurred while getting events.");
            }
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
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
                return evnt;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while getting an event.");
                throw new ApplicationException($"An error occurred while getting the event with ID {eventId}.");
            }
        }

        public async Task<bool> AddEventAsync(EventToAddDto eventToAdd)
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
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while inserting an event.");
                throw new ApplicationException($"An error occurred while inserting an event.");
            }
        }

        public async Task<bool> UpdateEventAsync(EventToAddDto eventToAdd, int eventId)
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
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while updating an event.");
                throw new ApplicationException($"An error occurred while updating the event with ID {eventId}");
            }
        }

        public async Task<bool> DeleteEventAsync(int eventId)
        {
            string sql = @"
                DELETE FROM EventBookingSchema.Events
                WHERE EventId = @EventIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@EventIdParam", eventId, DbType.Int32);

            try
            {
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an event.");
                throw new ApplicationException($"An error occurred while deleting the event with ID {eventId}");
            }
        }
    }
}
