using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task AddAsync(Product product);
        void Delete(Product product);
        Task SaveChangesAsync();
    }
}
