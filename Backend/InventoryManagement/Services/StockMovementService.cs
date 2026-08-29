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

        public async Task CreateStockMovementAsync(CreateStockMovementDto dto)
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

            // Adjustment movements must specify Increase or Decrease
            if (dto.MovementType == MovementType.Adjustment &&
                dto.AdjustmentType == null)
            {
                throw new Exception(
                    "Adjustment type must be specified for adjustment movements.");
            }

            // Purchase and Sale movements should not have an adjustment type
            if (dto.MovementType != MovementType.Adjustment &&
                dto.AdjustmentType != null)
            {
                throw new Exception(
                    "Adjustment type is only valid for adjustment movements.");
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
