using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly InventoryDbContext _context;
    public ProductRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken)
    {

        var products= await _context.Products
            .ToListAsync(cancellationToken);

        return products;
            
    }

    public async Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductId == productId, cancellationToken);

        return product;
    }

    public void Add(Product product)
    {
      _context.Products.Add(product);
    }

}
