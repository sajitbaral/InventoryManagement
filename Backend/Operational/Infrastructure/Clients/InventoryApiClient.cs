
using Operational.Application.DTOs.Inventory;
using Operational.Application.Interfaces;
using System.Net;
using System.Net.Http.Json;


namespace Operational.Infrastructure.Clients
{
    public class InventoryApiClient : IInventoryClient
    {
        private readonly HttpClient _httpClient;
        public InventoryApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    

    public async Task<ProductInfoResponse?> GetProduct(GetProductRequest request, CancellationToken cancellationToken)
        {
            using var response = await _httpClient.GetAsync(
                $"api/products/{request.ProductId}",
                cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            var product = await response.Content
                .ReadFromJsonAsync<ProductInfoResponse>(cancellationToken);

            return product ?? throw new InvalidOperationException(
                "Inventory API returned an empty product response.");
        }

        public async Task IncreaseStock(IncreaseStockRequest request, CancellationToken cancellationToken)
        {
            using var response = await _httpClient.PostAsJsonAsync("api/stocks/increase", request, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task DecreaseStock(DecreaseStockRequest request, CancellationToken cancellationToken)
        {
           using var response = await _httpClient.PostAsJsonAsync("api/stocks/decrease", request, cancellationToken);
            response.EnsureSuccessStatusCode() ;
        }
    }
}
