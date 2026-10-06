using Operational.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.DTOs.Sale
{
    public class SaleResponseDto
    {
        public int SaleId { get; set; }

        public int CustomerId { get; set; }
        public DateTime SaleDate { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<SaleItemResponseDto> Items { get; set; } = new();
    }
}
