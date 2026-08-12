namespace InventoryManagement.Dto
{
    public class PurchaseDto
    {
        public int SupplierId { get; set; }
        public List<PurchaseItemDto> Items { get; set; } = [];
    }
}
