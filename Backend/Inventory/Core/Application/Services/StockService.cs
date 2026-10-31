using Inventory.Application.DTOs.Stock;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProcessedStockOperationRepository _processedStockOperationRepository;


        public StockService(IStockRepository stockRepository, IStockMovementRepository stockMovementRepository, IProcessedStockOperationRepository processedStockOperationRepository, IUnitOfWork unitOfWork)
        {
            _stockRepository = stockRepository;
            _stockMovementRepository= stockMovementRepository;
            _unitOfWork= unitOfWork;
            _processedStockOperationRepository= processedStockOperationRepository;
        }

        public async Task<List<StockResponseDto>> GetStocksAsync(CancellationToken cancellationToken)
        {
            var stocks= await _stockRepository.GetStocksAsync(cancellationToken);

            return stocks.Select(s=> new StockResponseDto
            {
                StockId= s.StockId,
                ProductId= s.ProductId,
                Quantity = s.Quantity,
                LastUpdated= s.LastUpdated
            })
                .ToList();
            
        }

        public async Task<StockResponseDto?> GetByIdAsync(int stockId, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByIdAsync(stockId, cancellationToken);
            if (stock == null)
            {
                return null;
            }
            return new StockResponseDto
            {
                StockId = stock.StockId,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                LastUpdated = stock.LastUpdated
            };
                
        }

        public async Task<StockResponseDto?> GetByProductIdAsync(int productId,  CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByProductIdAsync(productId, cancellationToken);

            if(stock == null)
            {
                return null;
            }
            return new StockResponseDto
            {
                StockId = stock.StockId,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                LastUpdated = stock.LastUpdated
            };
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
                    ReferenceId = dto.ReferenceId,
                    MovementDate = DateTime.UtcNow,
                };

                _stockMovementRepository.Add(stockMovement);
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

                _stockMovementRepository.Add(stockMovement);
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

        public async Task <StockResponseDto> AdjustmentStockAsync(AdjustmentStockDto dto,  CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByProductIdAsync(dto.ProductId, cancellationToken);

            if(stock == null)
            {
                throw new Exception("Stock not found.");
            }
            if (dto.AdjustmentType == Domain.Enums.AdjustmentType.Increase)
            {
                try
                {
                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    stock.Quantity += dto.Quantity;
                    stock.LastUpdated = DateTime.UtcNow;



                    var stockMovement = new StockMovement
                    {
                        ProductId = dto.ProductId,
                        MovementType = Domain.Enums.MovementType.Adjustment,
                        Quantity = dto.Quantity,
                        MovementDate = DateTime.UtcNow,
                        AdjustmentType = dto.AdjustmentType,
                    };

                    _stockMovementRepository.Add(stockMovement);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    throw;
                }
            }

            else if (dto.AdjustmentType == Domain.Enums.AdjustmentType.Decrease)
            {
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
                        MovementType = Domain.Enums.MovementType.Adjustment,
                        Quantity = dto.Quantity,
                        MovementDate = DateTime.UtcNow,
                        AdjustmentType= dto.AdjustmentType
                    };

                    _stockMovementRepository.Add(stockMovement);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    throw;
                }
            }

            return new StockResponseDto
            {
                StockId = stock.StockId,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                LastUpdated = stock.LastUpdated
            };
        }

        public async Task ReserveStockForSaleAsync(ReserveStockForSaleRequest request,  CancellationToken cancellationToken)
        {
            var alreadyProcessed = await _processedStockOperationRepository.ExistsAsync(request.OperationId, cancellationToken);

            if (alreadyProcessed)
            {
                return;
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                var operationAdded = await _processedStockOperationRepository.TryAddAsync(new ProcessedStockOperation          /*This method is first Because OperationId is the primary key, SQL Server won't allow two requests to claim the same operation. This eliminates two identical requests both start deducting stock before either records the operation.*/
                {
                    OperationId = request.OperationId,
                    SaleId = request.SaleId,
                }, cancellationToken);

                if (!operationAdded)
                {
                    await _unitOfWork.RollbackTransactionAsync(CancellationToken.None);
                    return;
                }

                foreach (var item in request.Items)
                {
                    var stock = await _stockRepository.GetByProductIdAsync(item.ProductId, cancellationToken);

                    if (stock == null)
                    {
                        throw new Exception(
                            $"Stock for product {item.ProductId} was not found.");
                    }

                    if (stock.Quantity < item.Quantity)
                    {
                        throw new Exception(
                            $"Insufficient stock for product {item.ProductId}.");
                    }

                    stock.Quantity -= item.Quantity;
                    stock.LastUpdated = DateTime.UtcNow;

                    _stockMovementRepository.Add(new StockMovement
                    {
                        ProductId = item.ProductId,
                        MovementType = MovementType.Sale,
                        Quantity = item.Quantity,
                        MovementDate = DateTime.UtcNow,
                        ReferenceId = request.SaleId
                    });
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }

            catch
            {
                await _unitOfWork.RollbackTransactionAsync(CancellationToken.None);
                throw;
            }
        }
    }
}
