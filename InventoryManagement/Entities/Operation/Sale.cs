using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entities.Operation
{
    public class Sale
    {
        public int SaleId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public List<SaleItem> SaleItems { get; set; } = new();
    }
}
