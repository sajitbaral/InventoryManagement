using InventoryManagement.Dto;


namespace InventoryManagement.IService
{
    public interface IStockService
    {
        Task<StockResponseDto> CreateStockAsync(CreateStockDto createStockDto);
        Task<List<StockResponseDto>> GetStocksAsync();
        Task<StockResponseDto?> GetStockByIdAsync(int id);
        Task<bool> UpdateStockAsync(int id, UpdateStockDto dto);
        Task<bool> DeleteStockAsync(int id);
    }
}
