namespace InventoryManagement.Entities.Inventory
{
    public class Stock
    {
        public int StockId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
