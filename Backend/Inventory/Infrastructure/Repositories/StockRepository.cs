using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Repositories;

public class StockRepository : IStockRepository
{
    private readonly InventoryDbContext _context;
    public StockRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<Stock?>GetByProductIdAsync(int productId, CancellationToken cancellationToken)
    {
        return await _context.Stocks
            .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);
    }

    public async Task<List<Stock>> GetByProductIdsAsync(IEnumerable<int> productIds, CancellationToken cancellationToken)
    {
        return await _context.Stocks
            .Where(s=>productIds.Contains(s.ProductId))
            .ToListAsync(cancellationToken);
    }
}
