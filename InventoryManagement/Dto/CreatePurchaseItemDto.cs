using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class CreatePurchaseItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
