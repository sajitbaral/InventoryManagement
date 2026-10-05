using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken);
        Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken);
        void Add(Product product);
    }
}
