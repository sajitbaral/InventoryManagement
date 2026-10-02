using Operational.Application.Interfaces;
using Operational.Domain.Entities;
using Operational.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly OperationalDbContext _context;

        public SupplierRepository(OperationalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Supplier>> GetSuppliersAsync(
            CancellationToken cancellationToken)
        {
            var suppliers = await _context.Suppliers
                .ToListAsync(cancellationToken);
            return suppliers;
        }

        public async Task<Supplier?> GetSupplierByIdAsync(
            int supplierId,
            CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(
                    s => s.SupplierId == supplierId,
                    cancellationToken);

            return supplier;
        }

        public void Add(Supplier supplier)
        {
           _context.Suppliers.Add(supplier);
        }
    }
}
