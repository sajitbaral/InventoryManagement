using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class CreateSaleItemDto
    {

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        /*[Required]
        public decimal UnitPrice { get; set; }       This will allow client to send any price other than product.price*/          


    }
}
