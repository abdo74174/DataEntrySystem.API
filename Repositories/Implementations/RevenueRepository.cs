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
    public class RevenueRepository : IRevenueRepository
    {
        private readonly AppDbContext _context;

        public RevenueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Revenue>> GetAllAsync()
        {
            return await _context.Revenues.OrderByDescending(r => r.Date).ToListAsync();
        }

        public async Task<Revenue?> GetByIdAsync(int id)
        {
            return await _context.Revenues.FindAsync(id);
        }

        public async Task CreateAsync(Revenue revenue)
        {
            await _context.Revenues.AddAsync(revenue);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Revenue revenue)
        {
            _context.Revenues.Update(revenue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var revenue = await _context.Revenues.FindAsync(id);
            if (revenue != null)
            {
                _context.Revenues.Remove(revenue);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Revenue>> GetFilteredAsync(string? search, DateTime? from, DateTime? to)
        {
            var query = _context.Revenues.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(r => r.ClientName.Contains(search) || r.OperationType.Contains(search));
            }

            if (from.HasValue)
            {
                query = query.Where(r => r.Date >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(r => r.Date <= to.Value);
            }

            return await query.OrderByDescending(r => r.Date).ToListAsync();
        }
    }
}
