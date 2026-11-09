using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Abstractions.Messaging
{
    public interface IOutboxMessageUpdater
    {
        Task <bool> MarkAsProcessedAsync(Guid messageId, Guid lockToken, CancellationToken cancellationToken);
    }
}
