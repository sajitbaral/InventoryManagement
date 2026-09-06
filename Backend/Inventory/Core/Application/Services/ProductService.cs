using Inventory.Application.DTOs.Products;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        public ProductService(IProductRepository productRepository, IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
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
                    StockQuantity = stock
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
                StockQuantity = stock?.Quantity ?? 0
            };
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = dto.Name,
                SKU = dto.SKU,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow

            };

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync(cancellationToken);

            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                SKU = product.SKU,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                StockQuantity = 0
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

            await _productRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteProductAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);

            if (product == null)
            {
                return false;
            }

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
