using EventBookingAPI.Dtos;
using EventBookingAPI.Models;

namespace EventBookingAPI.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<bool> AddRoleAsync(RoleToAddDto role);
        Task<bool> UpdateRoleAsync(RoleToAddDto role, int roleId);
    }
}
