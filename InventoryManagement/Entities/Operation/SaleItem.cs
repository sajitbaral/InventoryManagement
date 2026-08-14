using InventoryManagement.Entities.Inventory;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entities.Operation
{
    public class SaleItem
    {
        public int SaleItemId { get; set; }

        [Required]
        public int SaleId { get; set; }

        public Sale Sale { get; set; }= null!;

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public decimal UnitPrice { get; set; }

        public decimal SubTotal { get; set; }
    }
}
