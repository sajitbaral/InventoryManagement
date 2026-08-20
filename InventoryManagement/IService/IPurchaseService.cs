using InventoryManagement.Dto;

namespace InventoryManagement.IService
{
    public interface IPurchaseService
    {
        Task<PurchaseResponseDto> CreatePurchaseAsync(CreatePurchaseDto dto);
        Task<List<PurchaseResponseDto>> GetPurchasesAsync();
        Task<PurchaseResponseDto?> GetPurchaseByIdAsync(int purchaseId);
    }
}
