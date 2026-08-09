namespace InventoryManagement.Entities.Operation
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int CustomerId { get; set; }

        public DateTime SaleDate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
