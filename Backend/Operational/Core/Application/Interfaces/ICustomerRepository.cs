using Operational.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken);
        Task<Customer?> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken);
        Task AddAsync(Customer customer);
    }
}
