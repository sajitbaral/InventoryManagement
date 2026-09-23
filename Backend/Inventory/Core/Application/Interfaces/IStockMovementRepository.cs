using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface IStockMovementRepository
    {
        Task<List<StockMovement>> GetStockMovementsAsync(CancellationToken cancellationToken);
        Task<StockMovement?> GetByIdAsync(int stockMovementId, CancellationToken cancellationToken);
        Task <List<StockMovement>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task AddAsync(StockMovement stockMovement);

    }
}
