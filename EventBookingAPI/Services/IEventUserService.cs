using EventBookingAPI.Models;

namespace EventBookingAPI.Services
{
    public interface IEventUserService
    {
        Task<IEnumerable<EventUser>> GetEventUserAsync();
        Task<bool> AddEventUserAsync(EventUser eventUser);
        Task<bool> DeleteEventUser(EventUser eventUser);
        Task<bool> DeleteEventUserByUserIdAsync(int userId);
        Task<bool> DeleteEventUserByEventIdAsync(int eventId);
    }
}
