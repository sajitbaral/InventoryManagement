using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Operational.Application.DTOs.Inventory
{
    public class ProductInfoResponse
    {
        public int ProductId {  get; set; }

        [Required]
        public string Name {  get; set; } = string.Empty;
        public bool IsActive {  get; set; }
    }
}
