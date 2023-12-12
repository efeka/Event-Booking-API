using System.Data;
using Dapper;
using EventBookingAPI.Data;
using EventBookingAPI.Models;
using Microsoft.Data.SqlClient;

namespace EventBookingAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DataContextDapper _dapper;
        private readonly ILogger<UserService> _logger;

        public UserService(DataContextDapper dapper, ILogger<UserService> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Users";

            try
            {
                return await _dapper.LoadDataAsync<User>(sql);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while getting users.");
                throw new ApplicationException("An error occurred while getting users.");
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Users
                WHERE UserId = @UserIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParam", userId, DbType.Int32);

            try
            {
                return await _dapper.LoadDataSingleWithParametersAsync<User>(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while getting the user.");
                throw new ApplicationException($"An error occurred while getting the user with ID {userId}.");
            }
        }

        public async Task<bool> AddUserAsync(UserToAddDto user)
        {
            string sql = @"
                INSERT INTO EventBookingSchema.Users (
                    RoleId,
                    FirstName,
                    LastName,
                    Email
                ) VALUES (
                    @RoleIdParam,
                    @FirstNameParam,
                    @LastNameParam,
                    @EmailParam
                )";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@RoleIdParam", user.RoleId, DbType.Int32);
            sqlParameters.Add("@FirstNameParam", user.FirstName, DbType.String);
            sqlParameters.Add("@LastNameParam", user.LastName, DbType.String);
            sqlParameters.Add("@EmailParam", user.Email, DbType.String);

            try
            {
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while inserting a user.");
                throw new ApplicationException($"An error occurred while inserting a user");
            }
        }

        public async Task<bool> UpdateUserAsync(UserToAddDto user, int userId)
        {
            string sql = @"
                UPDATE EventBookingSchema.Users
                SET RoleId = @RoleIdParam,
                    FirstName = @FirstNameParam,
                    LastName = @LastNameParam,
                    Email = @EmailParam
                WHERE UserId = @UserIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParam", userId, DbType.Int32);
            sqlParameters.Add("@RoleIdParam", user.RoleId, DbType.Int32);
            sqlParameters.Add("@FirstNameParam", user.FirstName, DbType.String);
            sqlParameters.Add("@LastNameParam", user.LastName, DbType.String);
            sqlParameters.Add("@EmailParam", user.Email, DbType.String);

            try
            {
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while updating a user.");
                throw new ApplicationException($"An error occurred while updating the user with ID {userId}");
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            string sql = @"
                EXEC EventBookingSchema.spUsers_Delete @UserId = @UserIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParam", userId, DbType.Int32);

            try
            {
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a user.");
                throw new ApplicationException($"An error occurred while deleting the user with ID {userId}");
            }
        }
    }
}
