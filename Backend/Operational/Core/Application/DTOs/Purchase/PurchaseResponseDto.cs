using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.DTOs.Purchase
{
    public class PurchaseResponseDto
    {
        public int PurchaseId { get; set; }
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<PurchaseItemResponseDto> Items { get; set; } = new();
    }
}
