
namespace Operational.Application.Abstractions.Messaging
{
    public record ClaimedOutboxMessage(
        Guid Id, 
        string Type, 
        string Payload, 
        Guid LockToken);
   
}
