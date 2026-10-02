using Microsoft.EntityFrameworkCore;
using Operational.Application.Interfaces;
using Operational.Domain.Entities;
using Operational.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly OperationalDbContext _context;
        public PurchaseRepository(OperationalDbContext context)
        {
            _context = context;
            
        }

        public async Task<List<Purchase>> GetPurchasesAsync(CancellationToken cancellationToken)
        {
            var purchases = await _context.Purchases
                .Include(p => p.PurchaseItems)
                .ToListAsync(cancellationToken);

            return purchases;
        }

        public async Task<Purchase?> GetPurchaseByIdAsync(int purchaseId, CancellationToken cancellationToken)
        {
            var purchase = await _context.Purchases
                .Include(p => p.PurchaseItems)
                .FirstOrDefaultAsync(p => p.PurchaseId == purchaseId, cancellationToken);
                

            return purchase;
        }

        public void Add(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
        }
    }
}
