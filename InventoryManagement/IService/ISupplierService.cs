using InventoryManagement.Dto;

namespace InventoryManagement.IService
{
    public interface ISupplierService
    {
        Task<SupplierResponseDto> CreateSupplierAsync(CreateSupplierDto dto);
        Task<List<SupplierResponseDto>> GetSupplierAsync();
        Task<SupplierResponseDto?> GetSupplierByIdAsync(int supplierId);
        
    }
}
