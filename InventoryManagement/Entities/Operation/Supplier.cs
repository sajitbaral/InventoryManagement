using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entities.Operation
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;


        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
