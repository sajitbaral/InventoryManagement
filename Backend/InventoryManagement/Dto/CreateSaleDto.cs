using InventoryManagement.Entities.Operation;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class CreateSaleDto
    {
        [Required]
        public int CustomerId { get; set; }

        public List<CreateSaleItemDto> Items { get; set; } = new();
    }
}
