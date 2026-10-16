using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Abstractions.Messaging
{
    public interface IOutboxWriter
    {
        void Add(OutboxMessageDraft message);
    }
}
