using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class CreatePurchaseDto
    {
        [Required]
        public int SupplierId { get; set; }
        public List<CreatePurchaseItemDto> Items { get; set; } = new();
    }
}
