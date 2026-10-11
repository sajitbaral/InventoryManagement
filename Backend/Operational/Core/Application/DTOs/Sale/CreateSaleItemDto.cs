
using System.ComponentModel.DataAnnotations;


namespace Operational.Application.DTOs.Sale
{
    public class CreateSaleItemDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
