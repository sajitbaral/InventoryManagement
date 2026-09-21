using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.DTOs.Stock
{
    public class StockResponseDto
    {
        public int StockId { get; set; }
        public int ProductId {  get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
