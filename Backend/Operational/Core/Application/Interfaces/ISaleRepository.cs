using Operational.Domain.Entities;

namespace Operational.Application.Interfaces
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetSalesAsync(CancellationToken cancellationToken);
        Task <Sale?> GetSaleByIdAsync(int saleId, CancellationToken cancellationToken);
        void Add(Sale sale);
    }
}
