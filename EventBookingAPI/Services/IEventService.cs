using EventBookingAPI.Models;

namespace EventBookingAPI.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event> GetEventByIdAsync(int eventId);
        Task<bool> AddEventAsync(EventToAddDto eventToAdd);
        Task<bool> UpdateEventAsync(EventToAddDto eventToAdd, int eventId);
        Task<bool> DeleteEventAsync(int eventId);
    }
}
