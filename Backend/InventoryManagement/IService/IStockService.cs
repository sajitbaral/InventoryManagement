using InventoryManagement.Dto;
using InventoryManagement.Entities.Inventory;


namespace InventoryManagement.IService
{
    public interface IStockService
    {
        /*Task<StockResponseDto> CreateStockAsync(CreateStockDto createStockDto);*/
        Task<List<StockResponseDto>> GetStocksAsync();
        Task<StockResponseDto?> GetStockByIdAsync(int stockId);

        Task<StockResponseDto> IncreaseStockAsync(int stockId, int quantity, int purchaseId);
        Task<StockResponseDto> DecreaseStockAsync(int stockId, int quantity, int saleId);
        Task<StockResponseDto> AdjustStockAsync(int stockId, int quantity, AdjustmentType adjustmentType);
        Task<bool> HasSufficientStockAsync(int productId, int quantity);



    }
}
