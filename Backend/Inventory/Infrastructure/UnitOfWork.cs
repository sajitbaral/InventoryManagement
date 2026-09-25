using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Inventory.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _transaction= await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
            {
              await _transaction.CommitAsync(cancellationToken);
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
        }
    }
}
