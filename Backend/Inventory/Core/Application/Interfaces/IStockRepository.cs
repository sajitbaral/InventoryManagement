using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task<List<Stock>> GetByProductIdsAsync(IEnumerable<int> productIds, CancellationToken cancellationToken);

    }
}
