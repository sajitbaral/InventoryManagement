namespace InventoryManagement.Dto
{
    public class UpdateStockDto
    {
        public int Quantity { get; set; } /*only quantity is needed as we are updating existing stock and we already know which stock from url (PUT /api/stock/5)*/
    }
}
