using Operational.Application.DTOs.Inventory;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

    namespace Operational.Application.Interfaces
    {
        public interface IInventoryClient
        {
            Task<ProductInfoResponse> GetProduct(GetProductRequest request, CancellationToken cancellationToken);
            Task IncreaseStock(IncreaseStockRequest request, CancellationToken cancellationToken);
        }
    }
