using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Repositories
{
    public class ProcessedStockOperationRepository : IProcessedStockOperationRepository
    {
        private readonly InventoryDbContext _context;
        public ProcessedStockOperationRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task <bool> ExistsAsync(Guid operationId, CancellationToken cancellationToken)
        {
            return await _context.ProcessedStockOperations
                .AnyAsync(x => x.OperationId == operationId, cancellationToken);
        }

        public void Add(ProcessedStockOperation operation)
        {
            _context.ProcessedStockOperations.Add(operation);
        }


    }
}
