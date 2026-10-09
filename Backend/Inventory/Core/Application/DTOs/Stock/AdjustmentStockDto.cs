using Inventory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

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
