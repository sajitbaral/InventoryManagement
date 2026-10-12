using Operational.Application.DTOs.Inventory;


    namespace Operational.Application.Interfaces
    {
        public interface IInventoryClient
        {
            Task<ProductInfoResponse?> GetProduct(GetProductRequest request, CancellationToken cancellationToken);
            Task IncreaseStock(IncreaseStockRequest request, CancellationToken cancellationToken);
            Task DecreaseStock(DecreaseStockRequest request, CancellationToken cancellationToken);
        }
    }
