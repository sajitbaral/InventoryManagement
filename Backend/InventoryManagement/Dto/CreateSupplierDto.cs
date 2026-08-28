using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class CreateSupplierDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;


        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

    }
}
