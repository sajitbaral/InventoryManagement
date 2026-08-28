using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class SaleItemResponseDto
    {
        public int SaleItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
