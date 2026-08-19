using InventoryManagement.Data;
using InventoryManagement.Dto;
using InventoryManagement.Entities.Inventory;
using InventoryManagement.IService;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly InventoryDbContext _context;
        public StockMovementService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<StockMovementResponseDto> CreateStockMovementAsync(CreateStockMovementDto dto)
        {
            if(dto.Quantity<= 0)
            {
                throw new Exception("Quantity must be greater than 0");
            }

            var productExists = await _context.Products
                .AnyAsync(p => p.ProductId == dto.ProductId);

            if (!productExists)
            {
                throw new Exception("Product not found.");
            }

            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s=> s.ProductId == dto.ProductId);

            if (stock == null)
            {
                throw new Exception("Stock for this product not found.");
            }

            switch (dto.MovementType)
            {
                case MovementType.Purchase:
                    stock.Quantity += dto.Quantity; /* stock.Quantity = stock.Quantity + dto.Quantity */
                    break;

                case MovementType.Sale:

                    if (dto.Quantity > stock.Quantity)
                    {
                        throw new Exception("Insufficient stock.");
                    }
                    stock.Quantity -= dto.Quantity; /* stock.Quantity = stock.Quantity - dto.Quantity */
                    break;

                case MovementType.Adjustment:
                    if (dto.AdjustmentType== AdjustmentType.increase)
                    {
                        stock.Quantity += dto.Quantity; 
                    }
                    else if(dto.AdjustmentType == AdjustmentType.decrease)
                    {
                        stock.Quantity -= dto.Quantity;
                    }

                    else
                    {
                        throw new Exception("Adjustment type must be specified for adjustment movements.");
                    }
                    break;

            }

            var movement = new StockMovement
            {
                ProductId = dto.ProductId,
                MovementType = dto.MovementType,
                Quantity = dto.Quantity,
                MovementDate = DateTime.UtcNow,
                ReferenceId = dto.ReferenceId,
                AdjustmentType = dto.AdjustmentType
            };

            _context.StockMovements.Add(movement);
            await _context.SaveChangesAsync();

            return new StockMovementResponseDto
            {
                StockMovementId = movement.StockMovementId,
                ProductId = movement.ProductId,
                MovementType = movement.MovementType,
                Quantity = movement.Quantity,
                MovementDate = movement.MovementDate,
                ReferenceId = movement.ReferenceId,
                AdjustmentType = movement.AdjustmentType
            };

        }
        public async Task<List<StockMovementResponseDto>> GetStockMovementsAsync()
        {
            var movements = await _context.StockMovements
                .Select(m => new StockMovementResponseDto
                {
                    StockMovementId = m.StockMovementId,
                    ProductId = m.ProductId,
                    MovementType = m.MovementType,
                    Quantity = m.Quantity,
                    MovementDate = m.MovementDate,
                    ReferenceId = m.ReferenceId,
                    AdjustmentType = m.AdjustmentType
                })
                .ToListAsync();

            return movements;

        }

        public async Task<StockMovementResponseDto?>GetStockMovementByIdAsync(int stockmovementId)
        {
            var movement= await _context.StockMovements
                .Where(m=>m.StockMovementId==stockmovementId)
                .Select(m=>new StockMovementResponseDto
                {
                    StockMovementId = m.StockMovementId,
                    ProductId = m.ProductId,
                    MovementType = m.MovementType,
                    Quantity = m.Quantity,
                    MovementDate = m.MovementDate,
                    ReferenceId = m.ReferenceId,
                    AdjustmentType = m.AdjustmentType
                })
                .FirstOrDefaultAsync();

            return movement;

        }
    }
}
