
using System.ComponentModel.DataAnnotations;


namespace Inventory.Application.DTOs.Stock
{
    public class ReserveStockForSaleRequest
    {
        [Range(1, int.MaxValue)]
        public int SaleId {  get; set; }

        [MinLength(1)]
        public List<ReserveStockItemRequest> Items { get; set; } = new();

    }
}
