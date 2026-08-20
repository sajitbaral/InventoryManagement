namespace InventoryManagement.Dto
{
    public class PurchaseResponseDto
    {
        public int PurchaseId { get; set; }

        public int SupplierId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal TotalAmount { get; set; }

        public List<PurchaseItemResponseDto> Items { get; set; } = new();
    }
}
