using EventBookingAPI.Models;

namespace EventBookingAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int userId);
        Task<bool> AddUserAsync(UserToAddDto user);
        Task<bool> UpdateUserAsync(UserToAddDto user, int userId);
        Task<bool> DeleteUserAsync(int userId);
    }
}
