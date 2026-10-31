using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
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

        public async Task <bool> TryAddAsync(ProcessedStockOperation operation, CancellationToken cancellationToken)
        {
            _context.ProcessedStockOperations.Add(operation);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }

            catch(DbUpdateException ex)
                 when (ex.GetBaseException() is SqlException sqlException &&
                    (sqlException.Number == 2601 || sqlException.Number == 2627))
            {

                _context.Entry(operation).State = EntityState.Detached; /*EF Core may still track our attempted entity as Added. so detached.*/

                return false;
            }

        }


    }
}
