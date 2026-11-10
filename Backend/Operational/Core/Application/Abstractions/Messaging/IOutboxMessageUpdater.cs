using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Abstractions.Messaging
{
    public interface IOutboxMessageUpdater
    {
        Task <bool> MarkAsProcessedAsync(Guid messageId, Guid lockToken, CancellationToken cancellationToken);
        Task<bool> MarkAsFailedAsync(Guid messageId, Guid lockToken, string error, DateTime nextAttemptAtUtc, int maxAttempts, CancellationToken cancellationToken);
    }
}
