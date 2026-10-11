
using System.ComponentModel.DataAnnotations;


namespace Operational.Application.DTOs.Inventory
{
    public class DecreaseStockRequest
    {
        [Range(1, int.MaxValue)]
        public int ProductId {  get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity {  get; set; }

        [Range(1, int.MaxValue)]
        public int ReferenceId {  get; set; }
    }
}
