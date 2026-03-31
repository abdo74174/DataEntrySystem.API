using DataEntrySystem.API.Data;
using DataEntrySystem.API.Models.Entities;
using DataEntrySystem.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataEntrySystem.API.Repositories.Implementations
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _context.Expenses.OrderByDescending(e => e.Date).ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task CreateAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Expense>> GetFilteredAsync(string? search, DateTime? from, DateTime? to)
        {
            var query = _context.Expenses.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e => e.ExpenseType.Contains(search) || (e.Notes != null && e.Notes.Contains(search)));
            }

            if (from.HasValue)
            {
                query = query.Where(e => e.Date >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(e => e.Date <= to.Value);
            }

            return await query.OrderByDescending(e => e.Date).ToListAsync();
        }
    }
}
