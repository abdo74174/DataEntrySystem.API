using System;

namespace DataEntrySystem.API.Models.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string ExpenseType { get; set; } = string.Empty; // Salaries, Water, Electricity, Procedures, Other
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public int CreatedByUserId { get; set; }
        public virtual User? CreatedByUser { get; set; }
    }
}
