

namespace Operational.Application.Abstractions.Messaging
{
    public sealed record OutboxMessageDraft(string Type, string Payload);
    
}
