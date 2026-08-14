using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class CategoryResponseDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
