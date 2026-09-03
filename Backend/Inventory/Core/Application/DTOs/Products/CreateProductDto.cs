using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs.Products;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string SKU { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public string? Description {get; set;}
}
