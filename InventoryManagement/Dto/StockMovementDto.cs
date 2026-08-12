namespace InventoryManagement.Dto
{
    public class StockMovementDto
    {
        public int ProductId { get; set; }
        public string MovementType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int? ReferenceId { get; set; }
    }
}
