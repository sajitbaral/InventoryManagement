using Azure;
using Operational.Application.DTOs.Inventory;
using Operational.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Operational.Infrastructure.Clients
{
    public class InventoryApiClient : IInventoryClient
    {
        private readonly HttpClient _httpClient;
        public InventoryApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    

    public async Task<ProductInfoResponse> GetProduct(GetProductRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"api/products/{request.ProductId}", cancellationToken);

            response.EnsureSuccessStatusCode();

            var product = await response.Content.ReadFromJsonAsync<ProductInfoResponse>(cancellationToken);

            return product!;

        }

        public async Task IncreaseStock(IncreaseStockRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync("api/stocks/increase", request, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task DecreaseStock(DecreaseStockRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync("api/stocks/decrease", request, cancellationToken);
            response.EnsureSuccessStatusCode() ;
        }
    }
}
