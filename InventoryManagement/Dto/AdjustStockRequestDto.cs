using InventoryManagement.Entities.Inventory;

namespace InventoryManagement.Dto
{
    public class AdjustStockRequestDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public AdjustmentType AdjustmentType { get; set; }
    }
}
