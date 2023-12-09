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
        public async Task<IActionResult> GetUsers()
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Users";

            try
            {
                IEnumerable<User> users = await _dapper.LoadDataAsync<User>(sql);
                return Ok(users);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return NotFound("Could not retrieve users");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUsers(int userId)
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Users
                WHERE UserId = @UserIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParam", userId, DbType.Int32);

            try
            {
                IEnumerable<User> users = await _dapper.LoadDataWithParametersAsync<User>(sql, sqlParameters);
                return Ok(users);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return NotFound("Could not retrieve user");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(UserToAddDto user)
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
                if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
                    return Ok();
                else
                    return BadRequest("Failed to insert user");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return BadRequest("Failed to insert user");
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(UserToAddDto user, int userId)
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
                if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
                    return Ok();
                else
                    return BadRequest($"Failed to update user with ID {userId}");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return BadRequest($"Failed to update user with ID {userId}");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            string sql = @"
                DELETE FROM EventBookingSchema.Users
                WHERE UserId = @UserIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@UserIdParam", userId, DbType.Int32);

            try
            {
                if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
                    return Ok();
                else
                    return BadRequest($"Failed to delete user with ID {userId}");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return BadRequest($"Failed to delete user with ID {userId}");
            }
        }

    }
}
