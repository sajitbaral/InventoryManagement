using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface IProcessedStockOperationRepository
    {
        Task<bool> ExistsAsync(Guid operationId, CancellationToken cancellationToken);
        void Add(ProcessedStockOperation operation);
    }
}
