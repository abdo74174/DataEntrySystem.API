using DataEntrySystem.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataEntrySystem.API.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense?> GetByIdAsync(int id);
        Task CreateAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(int id);
        Task<IEnumerable<Expense>> GetFilteredAsync(string? search, DateTime? from, DateTime? to);
    }
}
