using System.Data;
using Dapper;
using EventBookingAPI.Data;
using EventBookingAPI.Models;
using Microsoft.Data.SqlClient;

namespace EventBookingAPI.Services
{
    public class EventUserService : IEventUserService
    {
        private readonly DataContextDapper _dapper;
        private readonly ILogger<EventUserService> _logger;

        public EventUserService(DataContextDapper dapper, ILogger<EventUserService> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EventUser>> GetEventUserAsync()
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.EventUser";

            try
            {
                IEnumerable<EventUser> eventUsers = await _dapper.LoadDataAsync<EventUser>(sql);
                return eventUsers;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while getting eventusers.");
                throw new ApplicationException("An error occurred while getting eventusers.");
            }
        }

        public async Task<bool> AddEventUserAsync(EventUser eventUser)
        {
            string sql = @"
                INSERT INTO EventBookingSchema.EventUser (
                    UserId,
                    EventId
                ) VALUES (
                    @UserIdParam,
                    @EventIdParam
                )";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParam", eventUser.UserId, DbType.Int32);
            sqlParameters.Add("@EventIdParam", eventUser.EventId, DbType.Int32);

            try
            {
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while inserting an eventuser.");
                throw new ApplicationException($"An error occurred while inserting an eventuser");
            }
        }

        public async Task<bool> DeleteEventUser(EventUser eventUser)
        {
            string sql = @"
                DELETE FROM EventBookingSchema.EventUser
                WHERE UserId = @UserIdParam AND 
                EventId = @EventIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParam", eventUser.UserId, DbType.Int32);
            sqlParameters.Add("@EventIdParam", eventUser.EventId, DbType.Int32);

            try
            {
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an eventuser.");
                throw new ApplicationException("An error occurred while deleting an eventuser.");
            }
        }

        public async Task<bool> DeleteEventUserByUserIdAsync(int userId)
        {
            string sql = @"
                DELETE FROM EventBookingSchema.EventUser
                WHERE UserId = @UserIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParam", userId, DbType.Int32);

            try
            {
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an eventuser by userId.");
                throw new ApplicationException($"An error occurred while deleting the eventuser by userId {userId}");
            }
        }

        public async Task<bool> DeleteEventUserByEventIdAsync(int eventId)
        {
            string sql = @"
                DELETE FROM EventBookingSchema.EventUser
                WHERE EventId = @EventIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@EventIdParam", eventId, DbType.Int32);

            try
            {
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an eventuser by eventId.");
                throw new ApplicationException($"An error occurred while deleting the eventuser by eventId {eventId}");
            }
        }
    }
}
