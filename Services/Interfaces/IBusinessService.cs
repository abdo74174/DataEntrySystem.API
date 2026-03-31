using DataEntrySystem.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataEntrySystem.API.Services.Interfaces
{
    public interface IBusinessService
    {
        // Revenues
        Task<IEnumerable<RevenueReadDto>> GetAllRevenuesAsync(string? search, DateTime? from, DateTime? to);
        Task CreateRevenueAsync(RevenueCreateDto revenueDto, int userId);
        Task UpdateRevenueAsync(int id, RevenueCreateDto revenueDto);
        Task DeleteRevenueAsync(int id);

        // Expenses
        Task<IEnumerable<ExpenseReadDto>> GetAllExpensesAsync(string? search, DateTime? from, DateTime? to);
        Task CreateExpenseAsync(ExpenseCreateDto expenseDto, int userId);
        Task UpdateExpenseAsync(int id, ExpenseCreateDto expenseDto);
        Task DeleteExpenseAsync(int id);

        // Reports
        Task<IEnumerable<MonthlyReportDto>> GetMonthlyReportsAsync(string? search, DateTime? from, DateTime? to);
        Task<DashboardSummaryDto> GetSummaryAsync(DateTime? from, DateTime? to);
    }
}
