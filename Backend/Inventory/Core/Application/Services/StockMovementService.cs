using Inventory.Application.DTOs.StockMovement;
using Inventory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IStockMovementRepository _stockMovementRepository;
        public StockMovementService(IStockMovementRepository stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository;
            
        }

        public async Task<List<StockMovementResponseDto>> GetStockMovementsAsync(CancellationToken cancellationToken)
        {
            var stockMovements= await _stockMovementRepository.GetStockMovementsAsync(cancellationToken);

            return stockMovements.Select(sm => new StockMovementResponseDto
            {
                StockMovementId = sm.StockMovementId,
                ProductId = sm.ProductId,
                MovementType = sm.MovementType,
                Quantity = sm.Quantity,
                MovementDate = sm.MovementDate,
                ReferenceId = sm.ReferenceId,
                AdjustmentType = sm.AdjustmentType
            })
                .ToList();
        }

        public async Task<StockMovementResponseDto?> GetByIdAsync(int stockMovementId, CancellationToken cancellationToken)
        {
            var stockMovement = await _stockMovementRepository.GetByIdAsync(stockMovementId, cancellationToken);

            if (stockMovement == null)
            {
                return null;
            }

            return new StockMovementResponseDto
            {
                StockMovementId = stockMovement.StockMovementId,
                ProductId = stockMovement.ProductId,
                MovementType = stockMovement.MovementType,
                Quantity = stockMovement.Quantity,
                MovementDate = stockMovement.MovementDate,
                ReferenceId = stockMovement.ReferenceId,
                AdjustmentType = stockMovement.AdjustmentType
            };
        }

        public async Task<List<StockMovementResponseDto>> GetByProductIdAsync(int productId,  CancellationToken cancellationToken)
        {
            var stockMovements = await _stockMovementRepository.GetByProductIdAsync(productId, cancellationToken);
            return stockMovements.Select(sm => new StockMovementResponseDto
            {
                StockMovementId = sm.StockMovementId,
                ProductId = sm.ProductId,
                MovementType = sm.MovementType,
                Quantity = sm.Quantity,
                MovementDate = sm.MovementDate,
                ReferenceId = sm.ReferenceId,
                AdjustmentType = sm.AdjustmentType
            })
                .ToList();
        }

    }
}
