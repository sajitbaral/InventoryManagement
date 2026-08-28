using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entities.Inventory
{
    public class StockMovement
    {
        public int StockMovementId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        [Required]
        public MovementType MovementType { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime MovementDate { get; set; }

        public int? ReferenceId { get; set; }

        public AdjustmentType? AdjustmentType { get; set; }
    }
}
