using InventoryManagement.Data;
using InventoryManagement.Dto;
using InventoryManagement.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.IService;

namespace InventoryManagement.Services
{
    public class StockService : IStockService
    {
        private readonly InventoryDbContext _inventoryContext;
        private readonly OperationDbContext _operationContext;
        private readonly IStockMovementService _stockMovementService;

        public StockService(InventoryDbContext inventoryContext, OperationDbContext operationContext, IStockMovementService stockMovementService)
        {
            _inventoryContext= inventoryContext;
            _operationContext= operationContext;
            _stockMovementService = stockMovementService;
            
        }

       /* public async Task<StockResponseDto> CreateStockAsync(CreateStockDto dto)
        {
            var productExists = await _context.Products
                .AnyAsync(p => p.ProductId == dto.ProductId);

            if (!productExists)
                throw new Exception("Product not found.");

            var stockExists = await _context.Stocks
                .AnyAsync(s=> s.ProductId == dto.ProductId);

            if (stockExists)
                throw new Exception("Stock for this product already exists.");

            var stock = new Stock
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                LastUpdated = DateTime.UtcNow
            };

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return new StockResponseDto
            {
                StockId = stock.StockId,
                ProductId= stock.ProductId,
                Quantity = stock.Quantity,
                LastUpdated = stock.LastUpdated
            };
        }
       */

        public async Task<List<StockResponseDto>> GetStocksAsync()
        {
            var stocks = await _inventoryContext.Stocks
                .Select(s => new StockResponseDto
                {
                    StockId = s.StockId,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity,
                    LastUpdated = s.LastUpdated

                })
                .ToListAsync();

            return stocks;
        }

        public async Task<StockResponseDto?> GetStockByIdAsync(int stockId)
        {
            var stock = await _inventoryContext.Stocks
                .Where(s => s.StockId == stockId)
                .Select(s => new StockResponseDto
                 {
                     StockId = s.StockId,
                     ProductId = s.ProductId,
                     Quantity = s.Quantity,
                     LastUpdated = s.LastUpdated
                 })
            .FirstOrDefaultAsync();

            return stock;


        }
        public async Task<StockResponseDto> IncreaseStockAsync(int productId, int quantity, int purchaseId)
        {
            if (quantity <= 0)
            {
                throw new Exception("Quantity must be greater than zero.");
            }

            var productExists = await _inventoryContext.Products
                .AnyAsync(p => p.ProductId == productId);
            if (!productExists)
            {
                throw new Exception("Product not found.");
            }

            var purchaseExists = await _operationContext.Purchases
                .AnyAsync(p => p.PurchaseId == purchaseId);

            if (!purchaseExists)
            {
                throw new Exception($"Purchase {purchaseId} not found.");
            }

            var stock = await _inventoryContext.Stocks
                .FirstOrDefaultAsync(s => s.ProductId == productId);

            if(stock == null)
            {
                stock = new Stock
                {
                    ProductId = productId,
                    Quantity = quantity,
                    LastUpdated = DateTime.UtcNow
                };

                _inventoryContext.Stocks.Add(stock);
            }

            else
            {
                stock.Quantity += quantity;
                stock.LastUpdated = DateTime.UtcNow;
            }

            await _stockMovementService.CreateStockMovementAsync(new CreateStockMovementDto
            {
                ProductId = productId,
                MovementType= MovementType.Purchase,
                Quantity = quantity,
                ReferenceId = purchaseId
            });

            await _inventoryContext.SaveChangesAsync();

            return new StockResponseDto
            {
                StockId = stock.StockId,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                LastUpdated = stock.LastUpdated
            };
        }  
        
        public async Task<StockResponseDto> DecreaseStockAsync(int productId, int quantity, int saleId)
        {
            if (quantity <= 0)
            {
                throw new Exception("Quantity must be greater than zero.");
            }

            var productExists = await _inventoryContext.Products
                .AnyAsync(p => p.ProductId == productId);

            if (!productExists)
            {
                throw new Exception("Product not found.");
            }

            var saleExists = await _operationContext.Sales
                .AnyAsync(s => s.SaleId == saleId);

            if (!saleExists)
            {
                throw new Exception($"Sale {saleId} not found.");
            }

            var stock = await _inventoryContext.Stocks
                .FirstOrDefaultAsync(s=>s.ProductId == productId);

            if (stock == null)
            {
                throw new Exception("Stock not found for the this product.");
            }

            if(quantity > stock.Quantity)
            {
                throw new Exception("Insufficient stock quantity.");
            }

            await using var transaction = await _inventoryContext.Database.BeginTransactionAsync();

            try
            {

                stock.Quantity -= quantity;
                stock.LastUpdated = DateTime.UtcNow;

                await _stockMovementService.CreateStockMovementAsync(new CreateStockMovementDto
                {
                    ProductId = productId,
                    MovementType = MovementType.Sale,
                    Quantity = quantity,
                    ReferenceId = saleId
                });

                await _inventoryContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return new StockResponseDto
                {

                    StockId = stock.StockId,
                    ProductId = stock.ProductId,
                    Quantity = stock.Quantity,
                    LastUpdated = stock.LastUpdated
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<StockResponseDto> AdjustStockAsync( int productId,int quantity,AdjustmentType adjustmentType)
        {
            if (quantity <= 0)
            {
                throw new Exception("Quantity must be greater than zero.");
            }

            var productExists = await _inventoryContext.Products
                .AnyAsync(p => p.ProductId == productId);

            if (!productExists)
            {
                throw new Exception("Product not found.");
            }

            var stock = await _inventoryContext.Stocks
                .FirstOrDefaultAsync(s => s.ProductId == productId);

            if (stock == null)
            {
                throw new Exception("Stock not found for this product.");
            }

            switch (adjustmentType)
            {
                case AdjustmentType.Increase:
                    stock.Quantity += quantity;
                    break;

                case AdjustmentType.Decrease:

                    if (quantity > stock.Quantity)
                    {
                        throw new Exception("Insufficient stock quantity.");
                    }

                    stock.Quantity -= quantity;
                    break;

                default:
                    throw new Exception("Invalid adjustment type.");
            }

            stock.LastUpdated = DateTime.UtcNow;

            await _stockMovementService.CreateStockMovementAsync(
                new CreateStockMovementDto
                {
                    ProductId = productId,
                    MovementType = MovementType.Adjustment,
                    Quantity = quantity,
                    AdjustmentType = adjustmentType
                });

            await _inventoryContext.SaveChangesAsync();

            return new StockResponseDto
            {
                StockId = stock.StockId,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                LastUpdated = stock.LastUpdated
            };
        }

        public async Task<bool> HasSufficientStockAsync(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new Exception("Quantity must be greater than zero.");
            }

            var stock = await _inventoryContext.Stocks
                .FirstOrDefaultAsync(s => s.ProductId == productId);

            if (stock == null)
            {
                throw new Exception("Stock not found for this product.");
            }
            return stock.Quantity >= quantity;
        }


    }

        
        
        /*public async Task<bool> UpdateStockAsync(int stockId, UpdateStockDto dto)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.StockId == stockId);

            if (stock == null)
                return false;

            stock.Quantity = dto.Quantity;
            stock.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteStockAsync(int stockId)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.StockId == stockId);

            if (stock == null)
                return false;

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return true;
        }
        */
    
}
