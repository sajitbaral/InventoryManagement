
namespace Operational.Application.Abstractions.Messaging
{
    public interface IOutboxMessageClaimer
    {
        Task<ClaimedOutboxMessage?> ClaimNextAsync(CancellationToken cancellationToken);
    }
}
