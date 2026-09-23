using Inventory.Application.DTOs.StockMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface IStockMovementService
    {
        Task<List<StockMovementResponseDto>> GetStockMovementsAsync(CancellationToken cancellationToken);
        Task<StockMovementResponseDto?> GetByIdAsync(int stockMovementId,  CancellationToken cancellationToken);
        Task<List<StockMovementResponseDto>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
    }
}
