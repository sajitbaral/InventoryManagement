
using Operational.Infrastructure.Persistence.Outbox;


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
        public DateTime LockedUntilUtc {  get; set; }           //Allocates the reserved time until it expires. Worker A claims the message at 10:00 AM and sets the lock to expire at 10:02 AM.Until then, another worker should not claim that message.
        public Guid LockToken { get; set; }                     //When Worker A claims the message, the system generates a unique token, such as ABC123.That token identifies Worker A's particular claim.
        public string? LastError {  get; set; }

    }
}
