using Operational.Domain.Entities;

namespace Operational.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken);
        Task<Customer?> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken);
        void Add(Customer customer);
    }
}
