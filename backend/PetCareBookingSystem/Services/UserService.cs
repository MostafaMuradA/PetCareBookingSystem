using Microsoft.AspNetCore.Identity;
using PetCareBookingSystem.Models;

namespace PetCareBookingSystem.Services
{
    public interface IUserService
    {
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<IList<string>> GetUserRolesAsync(string userId);
        Task<bool> AddToRoleAsync(string userId, string role);
        Task<bool> RemoveFromRoleAsync(string userId, string role);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> AddToRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> RemoveFromRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.RemoveFromRoleAsync(user, role);
            return result.Succeeded;
        }
    }
} 