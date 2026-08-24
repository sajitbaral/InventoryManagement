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


        [Required]
        public int Quantity { get; set; }
         
        [Required]
        public decimal UnitCost { get; set; }

        public decimal SubTotal { get; set; }
    }
}
