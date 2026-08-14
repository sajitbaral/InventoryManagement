using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class CustomerDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
