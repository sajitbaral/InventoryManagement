using InventoryManagement.Dto;
using InventoryManagement.IService;
using InventoryManagement.Data;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Entities.Inventory;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage;

namespace InventoryManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly InventoryDbContext _context;

        public ProductService(InventoryDbContext context)
        {
            _context = context;


        }

        public async Task<List<ProductResponseDto>> GetProductsAsync()
        {
            return await _context.Products
                .Select(p => new ProductResponseDto                     /* new ProductResponseDto means For every Product, create a new ProductResponseDto object. */
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    SKU = p.SKU,
                    Price = p.Price,
                    Description = p.Description,
                    CategoryId = p.CategoryId,

                    StockQuantity = _context.Stocks
                        .Where(s=> s.ProductId==p.ProductId)
                        .Select(s=> s.Quantity)
                        .FirstOrDefault()                   /* Get the stock quantity for the product. If no stock is found, return 0. And no FirstOrDefault is used as whole query is already asynchronous (return await _context....) */
                })
                .ToListAsync();
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int productId)
        {
            return await _context.Products
                  .Where(p => p.ProductId == productId)
                  .Select(p => new ProductResponseDto
                  {
                      ProductId = p.ProductId,
                      Name = p.Name,
                      SKU = p.SKU,
                      Price = p.Price,
                      Description = p.Description,
                      CategoryId = p.CategoryId,

                      StockQuantity = _context.Stocks
                        .Where(s => s.ProductId == p.ProductId)
                        .Select(s => s.Quantity)
                        .FirstOrDefault()
                  })
                  .FirstOrDefaultAsync();

        }
        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                SKU = dto.SKU,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };

            _context.Products.Add(product);         /* Add this Product to the Products DbSet. */

            await _context.SaveChangesAsync();      /* Save the changes to the database(SQL server). */

            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                SKU = product.SKU,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Description = product.Description
            };

        }
        public async Task <bool> UpdateProductAsync(int productId, ProductUpdateDto dto)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return false;       /* Product not found. */
            }
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;
            await _context.SaveChangesAsync();
            return true;        /* Product updated successfully. */
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                return false;       /* Product not found. */
            }
            _context.Products.Remove(product);      /* Remove the product from the Products DbSet. */
            await _context.SaveChangesAsync();      /* Save the changes to the database(SQL server). */
            return true;        /* Product deleted successfully. */
        }
    }
}

