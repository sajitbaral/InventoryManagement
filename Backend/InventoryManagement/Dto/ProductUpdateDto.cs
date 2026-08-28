using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class ProductUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
    }
}
