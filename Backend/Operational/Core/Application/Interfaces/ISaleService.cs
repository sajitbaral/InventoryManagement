using Operational.Application.DTOs.Sale;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Interfaces
{
    public interface ISaleService
    {
        Task<List<SaleResponseDto>> GetSalesAsync(CancellationToken cancellationToken);
        Task<SaleResponseDto?> GetSaleByIdAsync(int saleId, CancellationToken cancellationToken);
        Task<SaleResponseDto> CreateSaleAsync(CreateSaleDto dto, CancellationToken cancellationToken);
    }
}
