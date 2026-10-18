using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Abstractions.Messaging
{
    public sealed record ReserveStockForSaleMessage(int saleId, IReadOnlyList<ReserveStockForSaleItem> Items)
    {

    }

    public sealed record ReserveStockForSaleItem(int ProductId, int Quantity)
    {

    }
}

