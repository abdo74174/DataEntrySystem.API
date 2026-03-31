using System;

namespace DataEntrySystem.API.Models.Entities
{
    public class Revenue
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string OperationType { get; set; } = string.Empty; // Visa, Work Contract, Umrah, Hajj
        public decimal ContractPrice { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Remaining => OfferPrice - PaidAmount;
        public decimal RevenueAmount => OfferPrice - ContractPrice;
        public decimal Rest => OfferPrice - PaidAmount;
        public int CreatedByUserId { get; set; }
        public virtual User? CreatedByUser { get; set; }
    }
}
