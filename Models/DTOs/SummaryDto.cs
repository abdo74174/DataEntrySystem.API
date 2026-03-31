using System.Collections.Generic;

namespace DataEntrySystem.API.Models.DTOs
{
    public class MonthlyReportDto
    {
        public string MonthName { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Month { get; set; }
        public List<RevenueReadDto> Revenues { get; set; } = new();
        public List<ExpenseReadDto> Expenses { get; set; } = new();
        public decimal TotalRevenue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalRemaining { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal NetProfit => TotalProfit - TotalExpenses;
    }

    public class DashboardSummaryDto
    {
        public decimal TotalRevenue { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalRemaining { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal NetProfit { get; set; }
    }
}
