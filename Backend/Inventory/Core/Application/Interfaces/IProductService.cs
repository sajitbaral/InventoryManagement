using Inventory.Application.DTOs.Products;

namespace Inventory.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductResponseDto>> GetProductsAsync(CancellationToken cancellationToken);
    Task<ProductResponseDto?> GetProductByIdAsync(int productId, CancellationToken cancellationToken);      /*allowed to be null if not found(?)*/
    Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateProductAsync(int productId, ProductUpdateDto dto, CancellationToken cancellationToken);    /*allowed to be false if not found. It is like if product found update and if not then return false*/
    Task<bool> DeleteProductAsync(int productId, CancellationToken cancellationToken);

}
