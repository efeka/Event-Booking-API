using System.Data;
using Dapper;
using EventBookingAPI.Data;
using EventBookingAPI.Dtos;
using EventBookingAPI.Models;
using Microsoft.Data.SqlClient;

namespace EventBookingAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly DataContextDapper _dapper;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IConfiguration config, ILogger<RoleService> logger)
        {
            _dapper = new(config);
            _logger = logger;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Roles";

            try
            {
                return await _dapper.LoadDataAsync<Role>(sql);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while getting roles.");
                throw new ApplicationException("An error occurred while getting roles.");
            }
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Roles
                WHERE RoleId = @RoleIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@RoleIdParam", roleId, DbType.String);

            try
            {
                return await _dapper.LoadDataSingleWithParametersAsync<Role>(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while getting a role.");
                throw new ApplicationException($"An error occurred while getting the role with ID {roleId}.");
            }
        }

        public async Task<bool> AddRoleAsync(RoleToAddDto role)
        {
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
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while inserting a role.");
                throw new ApplicationException($"An error occurred while inserting a role.");
            }
        }

        public async Task<bool> UpdateRoleAsync(RoleToAddDto role, int roleId)
        {
            string sql = @"
                UPDATE EventBookingSchema.Roles
                SET RoleName = @RoleNameParam
                WHERE RoleId = @RoleIdParam";

            DynamicParameters sqlParameters = new();
            sqlParameters.Add("@RoleIdParam", roleId, DbType.Int32);
            sqlParameters.Add("@RoleNameParam", role.RoleName, DbType.String);

            try
            {
                return await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while updating a role.");
                throw new ApplicationException($"An error occurred while updating the role with ID {roleId}");
            }
        }

    }
}
