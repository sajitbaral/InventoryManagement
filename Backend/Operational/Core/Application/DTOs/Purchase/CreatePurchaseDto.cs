using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Operational.Application.DTOs.Purchase
{
    public class CreatePurchaseDto
    {
        [Range(1, int.MaxValue)]
        public int SupplierId { get; set; }

        [Required]
        [MinLength(1)]
        public List<CreatePurchaseItemDto> Items { get; set; } = new();
    }
}
