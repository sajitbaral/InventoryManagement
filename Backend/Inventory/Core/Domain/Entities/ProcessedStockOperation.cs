using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Entities
{
    public class ProcessedStockOperation
    {
        public Guid OperationId { get; set; }
        public int SaleId { get; set; }
    }
}
