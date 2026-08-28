using InventoryManagement.Dto;

namespace InventoryManagement.IService
{
    public interface ISaleService
    {
        Task<SaleResponseDto> CreateSaleAsync(CreateSaleDto dto);
        Task<List<SaleResponseDto>> GetSalesAsync();
        Task<SaleResponseDto?> GetSaleByIdAsync(int saleId);
    }
}
