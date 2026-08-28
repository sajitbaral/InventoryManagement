using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class UpdateCategoryDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
