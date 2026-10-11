using Operational.Domain.Entities;

namespace Operational.Application.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken);
        Task<Supplier?> GetSupplierByIdAsync(int supplierId, CancellationToken cancellationToken);
        void Add(Supplier supplier);
    }
}
