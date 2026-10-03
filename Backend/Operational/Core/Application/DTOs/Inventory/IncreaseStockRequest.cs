using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.DTOs.Inventory
{
    public class IncreaseStockRequest
    {
        public int ProductId {  get; set; }
        public int Quantity {  get; set; }
        public int ReferenceId {  get; set; }
    }
}
