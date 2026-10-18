using Microsoft.EntityFrameworkCore.Storage;
using Operational.Application.Interfaces;


namespace Operational.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OperationalDbContext _context;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(OperationalDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if(_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already active");
            }
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
            {
                try
                {
                    await _transaction.CommitAsync(cancellationToken);
                }
                finally                                                                     //transaction should be disposed after use and made it to null.
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
            {
                try
                {
                    await _transaction.RollbackAsync(cancellationToken);
                }
                finally
                {
                    await _transaction.DisposeAsync();
                    _transaction= null;
                }
            }
        }
    }
}
