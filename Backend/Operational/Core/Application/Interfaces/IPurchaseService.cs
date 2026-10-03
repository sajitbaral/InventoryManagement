using Operational.Application.DTOs.Purchase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Interfaces
{
    public interface IPurchaseService
    {
        Task<List<PurchaseResponseDto>> GetPurchasesAsync(CancellationToken cancellationToken);
        Task<PurchaseResponseDto?> GetPurchaseByIdAsync(int purchaseId, CancellationToken cancellationToken);
        Task<PurchaseResponseDto> CreatePurchaseAsync(CreatePurchaseDto dto, CancellationToken cancellationToken);
    }
}
