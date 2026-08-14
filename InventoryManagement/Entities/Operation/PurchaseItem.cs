using InventoryManagement.Entities.Inventory;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entities.Operation
{
    public class PurchaseItem
    {
        public int PurchaseItemId { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        public Purchase Purchase { get; set; } = null!;

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
