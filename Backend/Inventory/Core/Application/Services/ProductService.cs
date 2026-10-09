using Inventory.Application.DTOs.Product;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;

namespace Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IStockRepository stockRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductResponseDto>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsAsync(cancellationToken);

            if (products.Count == 0)
            {
                return [];
            }

            var productIds = products.Select(p => p.ProductId)
                .ToList();

            var stocks = await _stockRepository.GetByProductIdsAsync(productIds, cancellationToken);

            var stockByProductId = stocks.ToDictionary(s => s.ProductId, s => s.Quantity);

            return products.Select(product =>
            {
                stockByProductId.TryGetValue(product.ProductId, out var stock);

                return new ProductResponseDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    SKU = product.SKU,
                    Price = product.Price,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    StockQuantity = stock,
                    IsActive = product.IsActive,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt
                };
            }).ToList();
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);

            if (product == null)
            {
                return null;
            }

            var stock = await _stockRepository.GetByProductIdAsync(productId, cancellationToken);

            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                SKU = product.SKU,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                StockQuantity = stock?.Quantity ?? 0,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var product = new Product
            {
                Name = dto.Name,
                SKU = dto.SKU,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                CreatedAt = now,
                UpdatedAt = now,
                Stock = new Stock
                {
                    Quantity = 0,
                    LastUpdated= now
                }

            };

            _productRepository.Add(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                SKU = product.SKU,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                StockQuantity = product.Stock.Quantity,
                IsActive = product.IsActive,
                CreatedAt= product.CreatedAt,
                UpdatedAt= product.UpdatedAt
            };
        }

        public async Task<bool> UpdateProductAsync(int productId, ProductUpdateDto dto, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);

            if (product == null)
            {
                return false;
            }

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;
            product.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool>DeactivateProductAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);

            if (product == null)
            {
                return false;
            }

            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;

        }
        public async Task<bool>ActivateProductAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);

            if (product == null)
            {
                return false;
            }

            product.IsActive = true;
            product.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
