namespace InventoryManagement.Entities.Inventory
{
    public class StockMovement
    {
        public int StockMovementId { get; set; }

        public int ProductId { get; set; }

        public string MovementType { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public DateTime MovementDate { get; set; }

        public int? ReferenceId { get; set; }
    }
}
