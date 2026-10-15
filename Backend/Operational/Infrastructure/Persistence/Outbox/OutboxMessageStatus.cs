using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Infrastructure.Persistence
{
    public enum OutboxMessageStatus
    {
        Pending = 0,
        Processing = 1,
        Processed = 2,
        DeadLetter = 3
    }
}
