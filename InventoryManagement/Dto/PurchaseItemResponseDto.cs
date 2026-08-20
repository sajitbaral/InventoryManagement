namespace InventoryManagement.Dto
{
    public class PurchaseItemResponseDto
    {
        public int PurchaseItemId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal SubTotal { get; set; }
    }
}
