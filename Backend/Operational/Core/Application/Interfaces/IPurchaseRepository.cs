using Operational.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<List<Purchase>> GetPurchasesAsync(CancellationToken cancellationToken);
        Task<Purchase?> GetPurchaseByIdAsync(int purchaseId, CancellationToken cancellationToken);
        void Add(Purchase purchase);
    }
}
