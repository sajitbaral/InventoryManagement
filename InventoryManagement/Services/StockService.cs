using InventoryManagement.Data;
using InventoryManagement.Dto;
using InventoryManagement.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.IService;

namespace InventoryManagement.Services
{
    public class StockService : IStockService
    {
        private readonly InventoryDbContext _context;

        public StockService(InventoryDbContext context )
        {
            _context= context;
            
        }

        public async Task<StockResponseDto> CreateStockAsync(CreateStockDto dto)
        {
            var productExists = await _context.Products
                .AnyAsync(p => p.ProductId == dto.ProductId);

            if (!productExists)
                throw new Exception("Product not found.");

            var stockExists = await _context.Stocks
                .AnyAsync(s=> s.ProductId == dto.ProductId);

            if (!stockExists)
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


        public async Task<List<StockResponseDto>> GetStocksAsync()
        {
            var stocks = await _context.Stocks
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

        public async Task<StockResponseDto?> GetStockByIdAsync(int id)
        {
            var stock = await _context.Stocks
                .Where(s => s.StockId == id)
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
        public async Task<bool> UpdateStockAsync(int id, UpdateStockDto dto)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.StockId == id);

            if (stock == null)
                return false;

            stock.Quantity = dto.Quantity;
            stock.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.StockId == id);

            if (stock == null)
                return false;

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
