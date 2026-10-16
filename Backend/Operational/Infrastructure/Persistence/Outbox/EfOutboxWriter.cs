using Operational.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Infrastructure.Persistence.Outbox
{
    public sealed class EfOutboxWriter : IOutboxWriter
    {
        private readonly OperationalDbContext _dbContext;
        public EfOutboxWriter(OperationalDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public void Add(OutboxMessageDraft message)
        {
            var now = DateTime.UtcNow;
            var outboxMessage = new OutboxMessage
            {
                Type = message.Type,
                Payload = message.Payload,
                Status = OutboxMessageStatus.Pending,
                CreatedAtUtc = now,
                NextAttemptAtUtc = now
            };

            _dbContext.OutboxMessages.Add(outboxMessage);
        }

    }
}
