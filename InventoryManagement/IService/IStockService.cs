using InventoryManagement.Dto;


namespace InventoryManagement.IService
{
    public interface IStockService
    {
        Task<StockResponseDto> CreateStockAsync(CreateStockDto createStockDto);
        Task<List<StockResponseDto>> GetStocksAsync();
        Task<StockResponseDto?> GetStockByIdAsync(int stockId);
        Task<bool> UpdateStockAsync(int stockId, UpdateStockDto dto);
        Task<bool> DeleteStockAsync(int stockId);
    }
}
