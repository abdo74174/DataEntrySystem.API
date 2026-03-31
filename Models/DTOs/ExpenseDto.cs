using System;

namespace DataEntrySystem.API.Models.DTOs
{
    public class ExpenseCreateDto
    {
        public string ExpenseType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }

    public class ExpenseReadDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ExpenseType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
    }
}
