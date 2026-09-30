using Operational.Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerResponseDto>> GetCustomersAsync(CancellationToken cancellationToken);
        Task<CustomerResponseDto?> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken);
        Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto dto,  CancellationToken cancellationToken);
        Task <bool> UpdateCustomerAsync (int  customerId, UpdateCustomerDto dto, CancellationToken cancellationToken);
        Task <bool> DeactivateCustomerAsync(int customerId, CancellationToken cancellationToken);
    }
}
