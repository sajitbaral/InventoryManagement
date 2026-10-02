using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.ServerSentEvents;
using System.Text;

namespace Operational.Domain.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;
        public DateTime SaleDate { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt {  get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<SaleItem> SaleItems { get; set; } = new();
    }
}
