using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task<List<Stock>> GetByProductIdsAsync(IEnumerable<int> productIds, CancellationToken cancellationToken);
        Task<List<Stock>> GetStocksAsync(CancellationToken cancellationToken);
        Task<Stock?>GetByIdAsync(int stockId, CancellationToken cancellationToken);

    }
}
