using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entities.Operation
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; } = null!;

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; } = new();
    }
}
