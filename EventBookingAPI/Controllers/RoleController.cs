using EventBookingAPI.Data;
using EventBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<Role> GetRoles()
        {
            string sql = @"
                SELECT *
                FROM EventBookingSchema.Roles";
            return _dapper.LoadData<Role>(sql);
        }
    }
}
