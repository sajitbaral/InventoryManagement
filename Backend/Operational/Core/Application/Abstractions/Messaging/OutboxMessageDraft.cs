using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Abstractions.Messaging
{
    public sealed record OutboxMessageDraft(string Type, string Payload);
    
}
