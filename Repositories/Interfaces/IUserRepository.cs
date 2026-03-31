using DataEntrySystem.API.Models.Entities;
using System.Threading.Tasks;

namespace DataEntrySystem.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(int id);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task<bool> ExistsAsync(string username);
        Task<List<User>> GetAllAsync();
        Task DeleteAsync(User user);
    }
}
