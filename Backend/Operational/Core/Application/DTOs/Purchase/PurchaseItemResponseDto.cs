using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.DTOs.Purchase
{
    public class PurchaseItemResponseDto
    {
        public int PurchaseItemId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitCost { get; set; }

        public decimal SubTotal { get; set; }
    }
}
