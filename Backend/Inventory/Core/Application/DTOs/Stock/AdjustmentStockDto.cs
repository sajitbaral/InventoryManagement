using Inventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Inventory.Domain.Enums;
using System.Text;

namespace Inventory.Application.DTOs.Stock
{
    public class AdjustmentStockDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId {  get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity {  get; set; }
        public AdjustmentType AdjustmentType {  get; set; }
    }
}
