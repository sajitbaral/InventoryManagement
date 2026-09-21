using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs.Product;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string SKU { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
    public string? Description {get; set;}
}
