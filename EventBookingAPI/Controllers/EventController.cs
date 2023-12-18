using EventBookingAPI.Models;
using EventBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ILogger<EventController> _logger;

        public EventController(IEventService eventService, ILogger<EventController> logger)
        {
            _eventService = eventService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsAsync()
        {
            try
            {
                IEnumerable<Event> events = await _eventService.GetEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting events.");
                return NotFound("Could not retrieve events.");
            }
        }
        [HttpGet("/Events/Current")]
        public async Task<ActionResult<IEnumerable<Event>>> GetCurrentEventsAsync()
        {
            try
            {
                IEnumerable<Event> events = await _eventService.GetCurrentEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting current events.");
                return NotFound("Could not retrieve current events.");
            }
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<Event>> GetEventByIdAsync(int eventId)
        {
            try
            {
                Event evnt = await _eventService.GetEventByIdAsync(eventId);
                return Ok(evnt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting an event.");
                return NotFound($"An error occurred while getting the event with ID {eventId}.");
            }
        }

        [HttpGet("/Events/{search}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsBySearch(string search)
        {
            try
            {
                IEnumerable<Event> events = await _eventService.GetEventsBySearchAsync(search);
                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for an event.");
                return NotFound($"An error occurred while searching for an event with search param: {search}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEventAsync(EventToAddDto eventToAdd)
        {
            try
            {
                if (await _eventService.AddEventAsync(eventToAdd))
                    return Ok();
                else
                    return BadRequest("An error occurred while inserting an event.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting an event.");
                return BadRequest("An error occurred while inserting an event.");
            }
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEventAsync(EventToAddDto eventToAdd, int eventId)
        {
            try
            {
                if (await _eventService.UpdateEventAsync(eventToAdd, eventId))
                    return Ok();
                else
                    return BadRequest($"An error occurred while updating the event with ID {eventId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating an event.");
                return BadRequest($"An error occurred while updating the event with ID {eventId}");
            }
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventAsync(int eventId)
        {
            try
            {
                if (await _eventService.DeleteEventAsync(eventId))
                    return Ok();
                else
                    return BadRequest($"An error occurred while deleting the event with ID {eventId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a user.");
                return BadRequest($"An error occurred while deleting the event with ID {eventId}");
            }
        }
    }
}
