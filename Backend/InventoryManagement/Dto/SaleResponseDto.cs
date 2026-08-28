using InventoryManagement.Entities.Operation;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class SaleResponseDto
    {
        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItemResponseDto> Items { get; set; } = new();
    }
}
