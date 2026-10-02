using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Operational.Domain.Entities
{
    public class Customer
    {
            public int CustomerId { get; set; }

            [Required]
            public string Name { get; set; } = string.Empty;
            public string? Phone { get; set; }
            public string? Email { get; set; }

            public string? Address { get; set; }
            public bool IsActive { get; set; } = true;

            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
      
    }
}
