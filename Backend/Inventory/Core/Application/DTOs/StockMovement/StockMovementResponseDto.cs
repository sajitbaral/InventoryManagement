using Inventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventory.Application.DTOs.StockMovement
{
    public class StockMovementResponseDto
    {
        public int StockMovementId { get; set; }
        public int ProductId { get; set; }
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public int? ReferenceId { get; set; }
        public AdjustmentType? AdjustmentType { get; set; }
    }
}
