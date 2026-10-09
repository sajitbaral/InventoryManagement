using Inventory.Application.DTOs.Stock;

namespace Inventory.Application.Interfaces
{
    public interface IStockService
    {
        Task<List<StockResponseDto>> GetStocksAsync(CancellationToken cancellationToken);
        Task<StockResponseDto?>GetByIdAsync(int stockId, CancellationToken cancellationToken);
        Task<StockResponseDto?> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task <StockResponseDto> IncreaseStockAsync(IncreaseStockDto dto, CancellationToken cancellationToken);
        Task <StockResponseDto> DecreaseStockAsync(DecreaseStockDto dto, CancellationToken cancellationToken);
        Task <StockResponseDto> AdjustmentStockAsync(AdjustmentStockDto dto, CancellationToken cancellationToken);
    }
}
