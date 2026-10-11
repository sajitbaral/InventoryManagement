
using System.ComponentModel.DataAnnotations;


namespace Operational.Application.DTOs.Customer
{
    public class UpdateCustomerDto
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
