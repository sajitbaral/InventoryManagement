using Operational.Application.DTOs.Purchase;
using Operational.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Operational.Application.DTOs.Sale
{
    public class CreateSaleDto
    {
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }

        [Required]
        [MinLength(1)]
        public List<CreateSaleItemDto> Items { get; set; } = new();
    }
}
