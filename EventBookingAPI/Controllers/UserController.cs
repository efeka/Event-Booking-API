using System.Data;
using Dapper;
using EventBookingAPI.Data;
using EventBookingAPI.Models;
using EventBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EventBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            try
            {
                IEnumerable<User> users = await _userService.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting users.");
                return NotFound("Could not retrieve users");
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserAsync(int userId)
        {
            try
            {
                User user = await _userService.GetUserAsync(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the user.");
                return NotFound($"An error occurred while getting the user with ID {userId}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(UserToAddDto user)
        {
            try
            {
                if (await _userService.AddUserAsync(user))
                    return Ok();
                else
                    return BadRequest("An error occurred while inserting a user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting a user.");
                return BadRequest("An error occurred while inserting a user.");
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(UserToAddDto user, int userId)
        {
            try
            {
                if (await _userService.UpdateUserAsync(user, userId))
                    return Ok();
                else
                    return BadRequest($"An error occurred while updating the user with ID {userId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a user.");
                return BadRequest($"An error occurred while updating the user with ID {userId}");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            try
            {
                if (await _userService.DeleteUserAsync(userId))
                    return Ok();
                else
                    return BadRequest($"An error occurred while deleting the user with ID {userId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a user.");
                return BadRequest($"An error occurred while deleting the user with ID {userId}");
            }
        }

    }
}
