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

        public async Task<List<ProductResponseDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();

            var result = new List<ProductResponseDto>();

            foreach (var product in products)
            {
                var stock = await _stockRepository.GetByProductIdAsync(product.ProductId);

                result.Add(new ProductResponseDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    SKU = product.SKU,
                    Price = product.Price,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    StockQuantity = stock?.Quantity ?? 0
                });

            }

            return result;


        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
            {
                return null;
            }

            var stock = await _stockRepository.GetByProductIdAsync(productId);

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

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
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
            await _productRepository.SaveChangesAsync();

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

        public async Task<bool> UpdateProductAsync(int productId, ProductUpdateDto dto)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
            {
                return false;
            }

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;

            await _productRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
            {
                return false;
            }

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();

            return true;

        }
    }
}
