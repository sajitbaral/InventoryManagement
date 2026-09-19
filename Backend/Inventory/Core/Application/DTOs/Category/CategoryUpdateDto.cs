using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.DTOs.Category
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; }= string.Empty;
        public string? Description { get; set; } 
    }
}
