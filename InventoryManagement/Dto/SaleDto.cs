namespace InventoryManagement.Dto
{
    public class SaleDto
    {
        public int CustomerId { get; set; }
        public List<SaleItemDto> Items { get; set; } = [];
    }
}
