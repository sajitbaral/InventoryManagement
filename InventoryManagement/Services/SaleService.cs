using InventoryManagement.Data;
using InventoryManagement.Dto;
using InventoryManagement.Entities.Operation;
using InventoryManagement.IService;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class SaleService : ISaleService
    {
        private readonly OperationDbContext _operationContext;
        private readonly InventoryDbContext _inventoryContext;
        private readonly IStockService _stockService;
        public SaleService(OperationDbContext operationContext, InventoryDbContext inventoryContext, IStockService stockService)
        {
            _inventoryContext = inventoryContext;
            _operationContext = operationContext;
            _stockService = stockService;

        }

        public async Task<SaleResponseDto> CreateSaleAsync(CreateSaleDto dto)
        {
            var customerExists = await _operationContext.Customers
                .AnyAsync(c => c.CustomerId == dto.CustomerId);

            if (!customerExists)
            {
                throw new Exception("Customer doesnot exists!");
            }

            if(dto.Items==null || dto.Items.Count == 0)
            {
                throw new Exception("Sale must contain atleast one item.");
            }

            foreach (var item in dto.Items)
            {
                if (item.Quantity <= 0)
                {
                    throw new Exception("Qunatity must be greater than 0");
                }

                if (item.UnitPrice <= 0)
                {
                    throw new Exception("Unit price must be greater than 0");
                }

                var productExists = await _inventoryContext.Products
                    .AnyAsync(p => p.ProductId == item.ProductId);

                if (!productExists)
                {
                    throw new Exception($"Product {item.ProductId} not found");
                }
            }

            var sale = new Sale
            {
                CustomerId = dto.CustomerId,
                SaleDate = DateTime.UtcNow,
                TotalAmount = 0
            };

            foreach (var item in dto.Items)
            {
                var subTotal = item.Quantity * item.UnitPrice;
                var saleItem = new SaleItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    SubTotal = subTotal
                };

                sale.SaleItems.Add(saleItem);
            }

            sale.TotalAmount = sale.SaleItems
                .Sum(i => i.SubTotal);

            _operationContext.Sales.Add(sale);

            await _operationContext.SaveChangesAsync();

            foreach (var item in sale.SaleItems)
            {
                await _stockService.DecreaseStockAsync(item.ProductId, item.Quantity, sale.SaleId);

            }



            return new SaleResponseDto {
                SaleId = sale.SaleId,
                CustomerId = sale.CustomerId,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                Items = sale.SaleItems
                    .Select(i => new SaleItemResponseDto
                    {
                        SaleItemId = i.SaleItemId,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice= i.UnitPrice,
                        SubTotal= i.SubTotal
                    })
                    .ToList()
            };
        }

        public async Task<List<SaleResponseDto>> GetSalesAsync()
        {
            var sales = await _operationContext.Sales
                .Select(s => new SaleResponseDto
                {
                    SaleId = s.SaleId,
                    CustomerId = s.CustomerId,
                    SaleDate = s.SaleDate,
                    TotalAmount = s.TotalAmount,

                    Items = s.SaleItems
                        .Select(i => new SaleItemResponseDto
                        {
                            SaleItemId = i.SaleItemId,
                            ProductId = i.ProductId,
                            Quantity = i.Quantity,
                            UnitPrice = i.UnitPrice,
                            SubTotal = i.SubTotal
                        })
                        .ToList()
                })
                .ToListAsync();

            return sales;
        }

        public async Task<SaleResponseDto?>GetSaleByIdAsync(int saleId)
        {
            var sale = await _operationContext.Sales
                .Where(s => s.SaleId == saleId)
                .Select(s => new SaleResponseDto
                {
                    SaleId = s.SaleId,
                    CustomerId = s.CustomerId,
                    SaleDate = s.SaleDate,
                    TotalAmount = s.TotalAmount,

                    Items = s.SaleItems
                        .Select(i => new SaleItemResponseDto
                        {
                            SaleItemId = i.SaleItemId,
                            ProductId = i.ProductId,
                            Quantity = i.Quantity,
                            UnitPrice = i.UnitPrice,
                            SubTotal = i.SubTotal
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return sale;
        }
    }
    
      
}
