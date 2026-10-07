using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Operational.Application.DTOs.Inventory
{
    public class GetProductRequest
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
    }
}
