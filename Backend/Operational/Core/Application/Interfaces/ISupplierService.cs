using Operational.Application.DTOs.Supplier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<List<SupplierResponseDto>> GetSuppliersAsync(
        CancellationToken cancellationToken);

        Task<SupplierResponseDto?> GetSupplierByIdAsync(
            int supplierId,
            CancellationToken cancellationToken);

        Task<SupplierResponseDto> CreateSupplierAsync(
            CreateSupplierDto dto,
            CancellationToken cancellationToken);

        Task<bool> UpdateSupplierAsync(
            int supplierId,
            UpdateSupplierDto dto,
            CancellationToken cancellationToken);

        Task<bool> DeactivateSupplierAsync(
            int supplierId,
            CancellationToken cancellationToken);
    }
}
