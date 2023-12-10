using EventBookingAPI.Dtos;
using EventBookingAPI.Models;
using EventBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<EventController> _logger;

        public RoleController(IRoleService roleService, ILogger<EventController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRolesAsync()
        {
            try
            {
                IEnumerable<Role> roles = await _roleService.GetRolesAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting events.");
                return NotFound("Could not retrieve events.");
            }
        }

        [HttpGet("{roleId}")]
        public async Task<ActionResult<Role>> GetRoleByIdAsync(int roleId)
        {
            try
            {
                Role role = await _roleService.GetRoleByIdAsync(roleId);
                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting a role.");
                return BadRequest($"An error occurred while getting the role with ID {roleId}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleAsync(RoleToAddDto role)
        {
            try
            {
                if (await _roleService.AddRoleAsync(role))
                    return Ok();
                else
                    return BadRequest($"An error occurred while inserting a role.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting a role.");
                return BadRequest($"An error occurred while inserting a role.");
            }
        }

        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRoleAsync(RoleToAddDto role, int roleId)
        {
            try
            {
                if (await _roleService.UpdateRoleAsync(role, roleId))
                    return Ok();
                else
                    return BadRequest($"An error occurred while updating the role with ID {roleId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a role.");
                return BadRequest($"An error occurred while updating the role with ID {roleId}");
            }
        }
    }
}
