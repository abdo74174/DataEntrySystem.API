using DataEntrySystem.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataEntrySystem.API.Repositories.Interfaces
{
    public interface IRevenueRepository
    {
        Task<IEnumerable<Revenue>> GetAllAsync();
        Task<Revenue?> GetByIdAsync(int id);
        Task CreateAsync(Revenue revenue);
        Task UpdateAsync(Revenue revenue);
        Task DeleteAsync(int id);
        Task<IEnumerable<Revenue>> GetFilteredAsync(string? search, DateTime? from, DateTime? to);
    }
}
