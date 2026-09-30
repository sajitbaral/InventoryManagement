using Microsoft.EntityFrameworkCore.Storage;
using Operational.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
