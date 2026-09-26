using Inventory.Application.DTOs.Stock;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StockService(IStockRepository stockRepository, IStockMovementRepository stockMovementRepository, IUnitOfWork unitOfWork)
        {
            _stockRepository = stockRepository;
            _stockMovementRepository= stockMovementRepository;
            _unitOfWork= unitOfWork;
        }

        public async Task<StockResponseDto> IncreaseStockAsync(IncreaseStockDto dto, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByProductIdAsync(dto.ProductId, cancellationToken);
            if (stock == null)
            {
                throw new Exception("There is no stock for this product");
            }

            
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                stock.Quantity += dto.Quantity;
                stock.LastUpdated = DateTime.UtcNow;

                

                var stockMovement=  new StockMovement
                {
                    ProductId = dto.ProductId,
                    MovementType = Domain.Enums.MovementType.Purchase,
                    Quantity = dto.Quantity,
                    MovementDate = DateTime.UtcNow,
                };

                await _stockMovementRepository.AddAsync(stockMovement);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
           
            return new StockResponseDto
            {
                StockId= stock.StockId,
                ProductId= stock.ProductId,
                Quantity= stock.Quantity,
                LastUpdated= stock.LastUpdated
            };

        }

        public async Task <StockResponseDto> DecreaseStockAsync(DecreaseStockDto dto, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByProductIdAsync(dto.ProductId, cancellationToken);

            if (stock == null)
            {
                throw new Exception("Stock for this product not found.");
            }
            if (stock.Quantity < dto.Quantity)
            {
                throw new Exception("Insufficient Quantity for this product");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                stock.Quantity -= dto.Quantity;
                stock.LastUpdated = DateTime.UtcNow;

                var stockMovement = new StockMovement
                {
                    ProductId = dto.ProductId,
                    MovementType = Domain.Enums.MovementType.Sale,
                    Quantity = dto.Quantity,
                    MovementDate = DateTime.UtcNow,
                };

                await _stockMovementRepository.AddAsync(stockMovement);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }

            return new StockResponseDto
            {
                StockId = stock.StockId,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                LastUpdated = stock.LastUpdated
            };

        
        }
    }
}
