using Operational.Domain.Entities;


namespace Operational.Application.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<List<Purchase>> GetPurchasesAsync(CancellationToken cancellationToken);
        Task<Purchase?> GetPurchaseByIdAsync(int purchaseId, CancellationToken cancellationToken);
        void Add(Purchase purchase);
    }
}
