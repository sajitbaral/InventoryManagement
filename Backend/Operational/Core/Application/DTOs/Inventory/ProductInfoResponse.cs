using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Operational.Application.DTOs.Inventory
{
    public class ProductInfoResponse
    {
        [Range(1, int.MaxValue)]
        public int ProductId {  get; set; }

        [Required]
        public string Name {  get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Price {  get; set; }
        public bool IsActive {  get; set; }
    }
}
