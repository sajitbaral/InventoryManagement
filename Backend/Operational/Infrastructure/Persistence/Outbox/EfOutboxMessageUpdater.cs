using Microsoft.EntityFrameworkCore;
using Operational.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Infrastructure.Persistence.Outbox
{
    public class EfOutboxMessageUpdater: IOutboxMessageUpdater
    {
        private readonly OperationalDbContext _dbContext;

        public EfOutboxMessageUpdater(OperationalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> MarkAsProcessedAsync(Guid messageId, Guid lockToken, CancellationToken cancellationToken)
        {
            var affectedRows = await _dbContext.OutboxMessages
                .Where(x => x.Id == messageId &&
                        x.LockToken == lockToken &&
                        x.Status == OutboxMessageStatus.Processing)

                .ExecuteUpdateAsync(setters => setters
                                    .SetProperty(x => x.Status, OutboxMessageStatus.Processed)
                                    .SetProperty(x => x.ProcessedAtUtc, DateTime.UtcNow)
                                    .SetProperty(x => x.LockedUntilUtc, DateTime.MinValue)
                                    .SetProperty(x => x.LockToken, Guid.Empty),
                                    cancellationToken);

            return affectedRows == 1;
        }

        public async Task<bool> MarkAsFailedAsync(Guid messageId, Guid lockToken, string error, DateTime nextAttemptAtUtc, int maxAttempts, CancellationToken cancellationToken)
        {
            var affectedRows = await _dbContext.OutboxMessages
                .Where(x=> x.Id == messageId &&
                        x.LockToken ==lockToken &&
                        x.Status == OutboxMessageStatus.Processing)

                .ExecuteUpdateAsync(setters => setters
                                    .SetProperty(x=> x.AttemptCount, x=> x.AttemptCount + 1)
                                    .SetProperty(x=> x.LastError, error)
                                    .SetProperty(x=> x.NextAttemptAtUtc, nextAttemptAtUtc)
                                    .SetProperty(x=> x.Status,
                                                x=> x.AttemptCount + 1 > maxAttempts
                                                ? OutboxMessageStatus.DeadLetter
                                                :OutboxMessageStatus.Pending)
                                    .SetProperty(x=> x.LockedUntilUtc, DateTime.MinValue)
                                    .SetProperty(x=> x.LockToken, Guid.Empty),
                                    cancellationToken);

            return affectedRows == 1;
        }

    }
}
