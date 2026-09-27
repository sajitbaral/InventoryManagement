using System.ComponentModel.DataAnnotations;

namespace Inventory.Domain.Entities
{
    public class Stock
    {
        public int StockId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTime LastUpdated { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
    }
}
