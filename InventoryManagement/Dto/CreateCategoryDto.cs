using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        
    }
}
