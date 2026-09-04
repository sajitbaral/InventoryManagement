using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly InventoryDbContext _context;
    public ProductRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsAsync()
    {

        var products= await _context.Products
            .ToListAsync();

        return products;
            
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        return product;
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
