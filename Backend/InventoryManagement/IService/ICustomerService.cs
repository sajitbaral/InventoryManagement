using InventoryManagement.Dto;

namespace InventoryManagement.IService
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto dto);
        Task<List<CustomerResponseDto>> GetCustomersAsync();
        Task<CustomerResponseDto?> GetCustomerByIdAsync(int customerId);

    }
}
