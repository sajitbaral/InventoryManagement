using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.DTOs.Inventory
{
    public class ProductInfoResponseDto
    {
        public int ProductId {  get; set; }
        public string Name {  get; set; }
        public bool IsActive {  get; set; }
    }
}
