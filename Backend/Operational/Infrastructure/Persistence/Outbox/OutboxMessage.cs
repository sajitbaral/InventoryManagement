using Operational.Domain.Enums;
using Operational.Infrastructure.Persistence.Outbox;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Operational.Infrastructure.Persistence
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string Type { get; set; } = String.Empty;
        public string Payload {  get; set; }= String.Empty;
        public OutboxMessageStatus Status { get; set; } = OutboxMessageStatus.Pending;
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ProcessedAtUtc {  get; set; }
        public int AttemptCount {  get; set; }
        public DateTime NextAttemptAtUtc {  get; set; }
        public DateTime LockedUntilUtc {  get; set; }
        public Guid LockToken { get; set; }
        public string? LastError {  get; set; }

    }
}
