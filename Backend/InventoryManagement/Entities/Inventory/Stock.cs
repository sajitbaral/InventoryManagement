using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entities.Inventory
{
    public class Stock
    {
        public int StockId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        public DateTime LastUpdated { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
    }
}
