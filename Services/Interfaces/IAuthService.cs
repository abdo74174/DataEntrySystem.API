using DataEntrySystem.API.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataEntrySystem.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(LoginDto registerDto); 
        Task<bool> UpdateProfileAsync(int userId, UpdateProfileDto updateDto);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int userId);
    }
}
