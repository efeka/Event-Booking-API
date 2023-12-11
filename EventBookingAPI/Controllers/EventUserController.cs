using EventBookingAPI.Data;
using EventBookingAPI.Models;
using EventBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EventBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventUserController : ControllerBase
    {
        private readonly IEventUserService _eventUserService;
        private readonly ILogger<EventUserController> _logger;

        public EventUserController(IEventUserService eventUserService, ILogger<EventUserController> logger)
        {
            _eventUserService = eventUserService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventUser>>> GetEventUsersAsync()
        {
            try
            {
                IEnumerable<EventUser> eventUsers = await _eventUserService.GetEventUserAsync();
                return Ok(eventUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting eventusers.");
                return NotFound("An error occurred while getting eventusers.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEventUserAsync(EventUser eventUser)
        {
            try
            {
                if (await _eventUserService.AddEventUserAsync(eventUser))
                    return Ok();
                else
                    return BadRequest("An error occurred while inserting an eventuser.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting an eventuser.");
                return BadRequest("An error occurred while inserting an eventuser.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEventUserAsync(EventUser eventUser)
        {
            try
            {
                if (await _eventUserService.DeleteEventUser(eventUser))
                    return Ok();
                else
                    return BadRequest("An error occurred while deleting an eventuser.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an eventuser.");
                return BadRequest("An error occurred while deleting the eventuser.");
            }
        }

        [HttpDelete("/DeleteByUserId/{userId}")]
        public async Task<IActionResult> DeleteEventUserByUserIdAsync(int userId)
        {
            try
            {
                if (await _eventUserService.DeleteEventUserByUserIdAsync(userId))
                    return Ok();
                else
                    return BadRequest($"An error occurred while deleting the eventuser by userId {userId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an eventuser by userId.");
                return BadRequest($"An error occurred while deleting the eventuser by userId {userId}");
            }
        }

        [HttpDelete("/DeleteByEventId/{eventId}")]
        public async Task<IActionResult> DeleteEventUserByEventIdAsync(int eventId)
        {
            try
            {
                if (await _eventUserService.DeleteEventUserByEventIdAsync(eventId))
                    return Ok();
                else
                    return BadRequest($"An error occurred while deleting the eventuser by eventId {eventId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an eventuser by eventId.");
                return BadRequest($"An error occurred while deleting the eventuser by eventId {eventId}");
            }
        }
    }
}
