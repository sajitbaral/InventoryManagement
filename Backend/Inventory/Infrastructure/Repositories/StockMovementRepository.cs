using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Repositories
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly InventoryDbContext _context;
        public StockMovementRepository(InventoryDbContext context)
        {
            _context= context;
        }

        public async Task<List<StockMovement>> GetStockMovementsAsync(CancellationToken cancellationToken)
        {
            return await _context.StockMovements
                .ToListAsync(cancellationToken);

        }

        public async Task<StockMovement?> GetByIdAsync(int stockMovementId, CancellationToken cancellationToken)
        {
            var stockMovement= await _context.StockMovements
                .FirstOrDefaultAsync(sm=> sm.StockMovementId == stockMovementId, cancellationToken);

            return stockMovement;
        }

        public async Task<List<StockMovement>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var stockMovements = await _context.StockMovements
                .Where(sm => sm.ProductId == productId)
                .ToListAsync(cancellationToken);

            return stockMovements;

        }

        public async Task AddAsync(StockMovement stockMovement)
        {
            await _context.StockMovements.AddAsync(stockMovement);
        }

    }
}
