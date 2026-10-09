using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface IStockMovementRepository
    {
        Task<List<StockMovement>> GetStockMovementsAsync(CancellationToken cancellationToken);
        Task<StockMovement?> GetByIdAsync(int stockMovementId, CancellationToken cancellationToken);
        Task <List<StockMovement>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        void Add(StockMovement stockMovement);

    }
}
