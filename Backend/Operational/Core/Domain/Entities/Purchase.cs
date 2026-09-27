using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Operational.Domain.Entities
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; } = null!;

        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; } = new();
    }
}
