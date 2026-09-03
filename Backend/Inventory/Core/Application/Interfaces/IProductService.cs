using Inventory.Application.DTOs.Products;

namespace Inventory.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductResponseDto>> GetProductsAsync();
    Task<ProductResponseDto?> GetProductByIdAsync(int productId);      /*allowed to be null if not found(?)*/
    Task<ProductResponseDto> CreateProductAsync(CreateProductDto Dto);
    Task<bool> UpdateProductAsync(int productId, ProductUpdateDto Dto);    /*allowed to be false if not found. It is like if product found update and if not then return false*/
    Task<bool> DeleteProductAsync(int productId);

}
