using Inventory.Application.DTOs.Stock;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface IStockService
    {
        Task <StockResponseDto> IncreaseStockAsync(IncreaseStockDto dto, CancellationToken cancellationToken);
        Task <StockResponseDto> DecreaseStockAsync(DecreaseStockDto dto, CancellationToken cancellationToken);
        Task <StockResponseDto> AdjustmentStockAsync(AdjustmentStockDto dto, CancellationToken cancellationToken);
    }
}
