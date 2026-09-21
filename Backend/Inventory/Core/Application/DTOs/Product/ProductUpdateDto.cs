using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs.Product;

public class ProductUpdateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }

    public string? description { get; set; }
}
