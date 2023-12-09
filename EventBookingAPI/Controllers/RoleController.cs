using System.Data;
using Dapper;
using EventBookingAPI.Data;
using EventBookingAPI.Dtos;
using EventBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace EventBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        public RoleController(IConfiguration config)
        {
            _dapper = new(config);
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesAsync()
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Roles";

            try
            {
                IEnumerable<Role> roles = await _dapper.LoadDataAsync<Role>(sql);
                return Ok(roles);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return NotFound("Could not retrieve roles");
            }
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleByIdAsync(int roleId)
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Roles
                WHERE RoleId = @RoleIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@RoleIdParam", roleId, DbType.String);

            try
            {
                Role role = await _dapper.LoadDataSingleWithParametersAsync<Role>(sql, sqlParameters);
                return Ok(role);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return NotFound($"Could not find Role with ID {roleId}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleAsync(RoleToAddDto role)
        {
            if (string.IsNullOrEmpty(role?.RoleName))
                return BadRequest("RoleName cannot be null or empty");

            string sql = @"
                INSERT INTO EventBookingSchema.Roles (
                    RoleName
                ) VALUES (
                    @RoleNameParam
                )";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@RoleNameParam", role.RoleName, DbType.String);

            try
            {
                if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
                    return Ok();
                else
                    return BadRequest("Failed to insert role");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return BadRequest("Failed to insert role");
            }
        }

        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRoleAsync(RoleToAddDto role, int roleId)
        {
            if (string.IsNullOrEmpty(role?.RoleName))
                return BadRequest("RoleName cannot be null or empty");

            string sql = @"
                UPDATE EventBookingSchema.Roles
                SET RoleName = @RoleNameParam
                WHERE RoleId = @RoleIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@RoleIdParam", roleId, DbType.Int32);
            sqlParameters.Add("@RoleNameParam", role.RoleName, DbType.String);

            try
            {
                if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
                    return Ok();
                else
                    return BadRequest($"Failed to update role with ID {roleId}");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception)
            {
                return BadRequest($"Failed to update role with ID {roleId}");
            }
        }
    }
}
