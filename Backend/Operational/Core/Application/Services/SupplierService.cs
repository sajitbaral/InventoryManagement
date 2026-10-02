using Operational.Application.DTOs.Supplier;
using Operational.Application.Interfaces;
using Operational.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(
            ISupplierRepository supplierRepository,
            IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SupplierResponseDto>> GetSuppliersAsync(
            CancellationToken cancellationToken)
        {
            var suppliers = await _supplierRepository
                .GetSuppliersAsync(cancellationToken);

            return suppliers
                .Select(MapToResponseDto)
                .ToList();
        }

        public async Task<SupplierResponseDto?> GetSupplierByIdAsync(
            int supplierId,
            CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository
                .GetSupplierByIdAsync(supplierId, cancellationToken);

            if (supplier == null)
            {
                return null;
            }

            return MapToResponseDto(supplier);
        }

        public async Task<SupplierResponseDto> CreateSupplierAsync(
            CreateSupplierDto dto,
            CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var supplier = new Supplier
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                CreatedAt = now,
                UpdatedAt = now
            };

            _supplierRepository.Add(supplier);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToResponseDto(supplier);
        }

        public async Task<bool> UpdateSupplierAsync(
            int supplierId,
            UpdateSupplierDto dto,
            CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository
                .GetSupplierByIdAsync(supplierId, cancellationToken);

            if (supplier == null)
            {
                return false;
            }

            supplier.Name = dto.Name;
            supplier.Phone = dto.Phone;
            supplier.Email = dto.Email;
            supplier.Address = dto.Address;
            supplier.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeactivateSupplierAsync(
            int supplierId,
            CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository
                .GetSupplierByIdAsync(supplierId, cancellationToken);

            if (supplier == null)
            {
                return false;
            }

            supplier.IsActive = false;
            supplier.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> ActivateSupplierAsync(
               int supplierId,
               CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository
                .GetSupplierByIdAsync(supplierId, cancellationToken);

            if (supplier == null)
            {
                return false;
            }

            supplier.IsActive = true;
            supplier.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        private static SupplierResponseDto MapToResponseDto(
            Supplier supplier)
        {
            return new SupplierResponseDto
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address,
                IsActive = supplier.IsActive,
                CreatedAt = supplier.CreatedAt,
                UpdatedAt = supplier.UpdatedAt
            };
        }
    }
}
