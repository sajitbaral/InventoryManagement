
using System.ComponentModel.DataAnnotations;


namespace Operational.Application.DTOs.Sale
{
    public class CreateSaleDto
    {
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }

        [Required]
        [MinLength(1)]
        public List<CreateSaleItemDto> Items { get; set; } = new();
    }
}
