using Operational.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken);
        Task<Supplier?> GetSupplierByIdAsync(int supplierId, CancellationToken cancellationToken);
        Task AddAsync(Supplier supplier);
    }
}
