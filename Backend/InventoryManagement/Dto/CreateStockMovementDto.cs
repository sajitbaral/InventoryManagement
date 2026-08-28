using InventoryManagement.Entities.Inventory;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Dto
{
    public class CreateStockMovementDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public MovementType MovementType { get; set; }

        [Required]
        public int Quantity { get; set; }

        public AdjustmentType? AdjustmentType { get; set; }

        public int? ReferenceId { get; set; }

    }
}
