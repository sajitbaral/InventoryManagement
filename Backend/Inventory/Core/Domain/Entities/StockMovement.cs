using System.ComponentModel.DataAnnotations;
using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities
{
    public class StockMovement
    {
        public int StockMovementId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public MovementType MovementType { get; set; }

        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }

        public int? ReferenceId { get; set; }

        public AdjustmentType? AdjustmentType { get; set; }
    }
}
