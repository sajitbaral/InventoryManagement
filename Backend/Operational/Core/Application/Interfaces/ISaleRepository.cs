using Operational.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Interfaces
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetSalesAsync(CancellationToken cancellationToken);
        Task <Sale?> GetSaleByIdAsync(int saleId, CancellationToken cancellationToken);
        void Add(Sale sale);
    }
}
