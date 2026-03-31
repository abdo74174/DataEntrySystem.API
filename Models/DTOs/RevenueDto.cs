using System;

namespace DataEntrySystem.API.Models.DTOs
{
    public class RevenueCreateDto
    {
        public string ClientName { get; set; } = string.Empty;
        public string OperationType { get; set; } = string.Empty;
        public decimal ContractPrice { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal PaidAmount { get; set; }
    }

    public class RevenueReadDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string OperationType { get; set; } = string.Empty;
        public decimal ContractPrice { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Remaining { get; set; }
        public decimal Revenue { get; set; }
        public decimal Rest { get; set; }
    }
}
