using InventoryManagement.Dto;

namespace InventoryManagement.IService
{
    public interface IStockMovementService
    {
        Task<StockMovementResponseDto> CreateStockMovementAsync(CreateStockMovementDto dto);
        Task <List<StockMovementResponseDto>> GetStockMovementsAsync();
        Task<StockMovementResponseDto?> GetStockMovementByIdAsync(int stockmovementId);

    }
}
