using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.DTOs.Sale
{
    public class SaleItemResponseDto
    {
        public int SaleItemId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal SubTotal { get; set; }
    }
}
