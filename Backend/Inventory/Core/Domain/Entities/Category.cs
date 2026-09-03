using System.ComponentModel.DataAnnotations;

namespace Inventory.Domain.Entities
{
    public class Category
    {

        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
