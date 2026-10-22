

namespace Operational.Application.Abstractions.Messaging
{
    public interface IOutboxWriter
    {
        void Add(OutboxMessageDraft message);
    }
}
