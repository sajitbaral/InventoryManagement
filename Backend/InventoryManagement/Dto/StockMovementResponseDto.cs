using InventoryManagement.Entities.Inventory;

namespace InventoryManagement.Dto
{
    public class StockMovementResponseDto
    {
        public int StockMovementId { get; set; }
        public int ProductId { get; set; }
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }

        public AdjustmentType? AdjustmentType { get; set; }
        public int? ReferenceId { get; set; }
    }
}
