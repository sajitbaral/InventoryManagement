using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entities.Inventory
{
    public class Product
    {
        public int ProductId { get; set; }  /* it is primary key+int type. so it will be auto-generated and also int is non nullable*/

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string SKU { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;     /*This is a required navigation property. A navigation property is a C# property that lets you move from one entity to a related entity. We can do product.Category.Name and direclty get that product*/

        [Required]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
