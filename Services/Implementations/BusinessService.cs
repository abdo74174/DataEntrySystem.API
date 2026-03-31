using DataEntrySystem.API.Models.DTOs;
using DataEntrySystem.API.Models.Entities;
using DataEntrySystem.API.Repositories.Interfaces;
using DataEntrySystem.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DataEntrySystem.API.Services.Implementations
{
    public class BusinessService : IBusinessService
    {
        private readonly IRevenueRepository _revenueRepository;
        private readonly IExpenseRepository _expenseRepository;

        public BusinessService(IRevenueRepository revenueRepository, IExpenseRepository expenseRepository)
        {
            _revenueRepository = revenueRepository;
            _expenseRepository = expenseRepository;
        }

        // Revenues
        public async Task<IEnumerable<RevenueReadDto>> GetAllRevenuesAsync(string? search, DateTime? from, DateTime? to)
        {
            var revenues = await _revenueRepository.GetFilteredAsync(search, from, to);
            return revenues.Select(MapToReadDto);
        }

        public async Task CreateRevenueAsync(RevenueCreateDto revenueDto, int userId)
        {
            var revenue = new Revenue
            {
                ClientName = revenueDto.ClientName,
                OperationType = revenueDto.OperationType,
                ContractPrice = revenueDto.ContractPrice,
                OfferPrice = revenueDto.OfferPrice,
                PaidAmount = revenueDto.PaidAmount,
                Date = DateTime.Now,
                CreatedByUserId = userId
            };
            await _revenueRepository.CreateAsync(revenue);
        }

        public async Task UpdateRevenueAsync(int id, RevenueCreateDto revenueDto)
        {
            var revenue = await _revenueRepository.GetByIdAsync(id);
            if (revenue != null)
            {
                revenue.ClientName = revenueDto.ClientName;
                revenue.OperationType = revenueDto.OperationType;
                revenue.ContractPrice = revenueDto.ContractPrice;
                revenue.OfferPrice = revenueDto.OfferPrice;
                revenue.PaidAmount = revenueDto.PaidAmount;
                await _revenueRepository.UpdateAsync(revenue);
            }
        }

        public async Task DeleteRevenueAsync(int id)
        {
            await _revenueRepository.DeleteAsync(id);
        }

        // Expenses
        public async Task<IEnumerable<ExpenseReadDto>> GetAllExpensesAsync(string? search, DateTime? from, DateTime? to)
        {
            var expenses = await _expenseRepository.GetFilteredAsync(search, from, to);
            return expenses.Select(MapToReadDto);
        }

        public async Task CreateExpenseAsync(ExpenseCreateDto expenseDto, int userId)
        {
            var expense = new Expense
            {
                ExpenseType = expenseDto.ExpenseType,
                Amount = expenseDto.Amount,
                Notes = expenseDto.Notes,
                Date = DateTime.Now,
                CreatedByUserId = userId
            };
            await _expenseRepository.CreateAsync(expense);
        }

        public async Task UpdateExpenseAsync(int id, ExpenseCreateDto expenseDto)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense != null)
            {
                expense.ExpenseType = expenseDto.ExpenseType;
                expense.Amount = expenseDto.Amount;
                expense.Notes = expenseDto.Notes;
                await _expenseRepository.UpdateAsync(expense);
            }
        }

        public async Task DeleteExpenseAsync(int id)
        {
            await _expenseRepository.DeleteAsync(id);
        }

        // Reports & Summaries
        public async Task<IEnumerable<MonthlyReportDto>> GetMonthlyReportsAsync(string? search, DateTime? from, DateTime? to)
        {
            var revenues = await _revenueRepository.GetFilteredAsync(search, from, to);
            var expenses = await _expenseRepository.GetFilteredAsync(search, from, to);

            var revenueGroups = revenues.GroupBy(r => new { r.Date.Year, r.Date.Month });
            var expenseGroups = expenses.GroupBy(e => new { e.Date.Year, e.Date.Month });

            var allMonths = revenueGroups.Select(g => g.Key)
                .Union(expenseGroups.Select(g => g.Key))
                .OrderByDescending(k => k.Year).ThenByDescending(k => k.Month);

            var reports = new List<MonthlyReportDto>();

            foreach (var key in allMonths)
            {
                var monthRevenues = revenues.Where(r => r.Date.Year == key.Year && r.Date.Month == key.Month).ToList();
                var monthExpenses = expenses.Where(e => e.Date.Year == key.Year && e.Date.Month == key.Month).ToList();

                reports.Add(new MonthlyReportDto
                {
                    Year = key.Year,
                    Month = key.Month,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(key.Month) + " " + key.Year,
                    Revenues = monthRevenues.Select(MapToReadDto).ToList(),
                    Expenses = monthExpenses.Select(MapToReadDto).ToList(),
                    TotalRevenue = monthRevenues.Sum(r => r.OfferPrice),
                    TotalPaid = monthRevenues.Sum(r => r.PaidAmount),
                    TotalRemaining = monthRevenues.Sum(r => r.Remaining),
                    TotalExpenses = monthExpenses.Sum(e => e.Amount),
                    TotalProfit = monthRevenues.Sum(r => r.RevenueAmount)
                });
            }

            return reports;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync(DateTime? from, DateTime? to)
        {
            var revenues = await _revenueRepository.GetFilteredAsync(null, from, to);
            var expenses = await _expenseRepository.GetFilteredAsync(null, from, to);

            return new DashboardSummaryDto
            {
                TotalRevenue = revenues.Sum(r => r.OfferPrice),
                TotalPaid = revenues.Sum(r => r.PaidAmount),
                TotalRemaining = revenues.Sum(r => r.Remaining),
                TotalExpenses = expenses.Sum(e => e.Amount),
                TotalProfit = revenues.Sum(r => r.RevenueAmount),
                NetProfit = revenues.Sum(r => r.RevenueAmount) - expenses.Sum(e => e.Amount)
            };
        }

        private RevenueReadDto MapToReadDto(Revenue revenue) => new()
        {
            Id = revenue.Id,
            ClientName = revenue.ClientName,
            Date = revenue.Date,
            OperationType = revenue.OperationType,
            ContractPrice = revenue.ContractPrice,
            OfferPrice = revenue.OfferPrice,
            PaidAmount = revenue.PaidAmount,
            Remaining = revenue.Remaining,
            Revenue = revenue.RevenueAmount,
            Rest = revenue.Rest
        };

        private ExpenseReadDto MapToReadDto(Expense expense) => new()
        {
            Id = expense.Id,
            Date = expense.Date,
            ExpenseType = expense.ExpenseType,
            Amount = expense.Amount,
            Notes = expense.Notes
        };
    }
}
