using InventoryManagement.Data;
using InventoryManagement.Dto;
using InventoryManagement.Entities.Operation;
using InventoryManagement.Entities.Inventory;
using InventoryManagement.IService;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly OperationDbContext _operationContext;
        private readonly InventoryDbContext _inventoryContext;
        private readonly IStockService _stockService;

        public PurchaseService(OperationDbContext operationContext, InventoryDbContext inventoryContext, IStockService stockService)
        {
            _operationContext = operationContext;
            _inventoryContext = inventoryContext;
            _stockService = stockService;
        }

        public async Task<PurchaseResponseDto>CreatePurchaseAsync(CreatePurchaseDto dto)
        {
            var supplierExists = await _operationContext.Suppliers
                .AnyAsync(s => s.SupplierId == dto.SupplierId);

            if (!supplierExists)
            {
                throw new Exception ( "Supplier not found" );
            }

            if(dto.Items==null || dto.Items.Count == 0)             /*null = no list was provided, Count == 0 = list exists but is empty..*/
            {
                throw new Exception ( "Purchase must contain atleast one item" );
            }

            foreach(var item in dto.Items)
            {
                if (item.Quantity <= 0)
                {
                    throw new Exception ( "Quantity must be more than 1" );
                }

                if(item.UnitCost<= 0)
                {
                    throw new Exception("Unit cost must be greater than 0");
                }

                var productExists = await _inventoryContext.Products
                    .AnyAsync(p => p.ProductId == item.ProductId);

                if (!productExists)
                {
                    throw new Exception($"Product {item.ProductId} not found");
                }
            }

       

            var purchase = new Purchase
            {
                SupplierId = dto.SupplierId,
                PurchaseDate = DateTime.UtcNow,
                TotalAmount = 0
            };

            foreach(var item in dto.Items)
            {
                var subTotal = item.Quantity * item.UnitCost;
                var purchaseItem = new PurchaseItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitCost = item.UnitCost,
                    SubTotal = subTotal
                };

          

                purchase.PurchaseItems.Add(purchaseItem);
            }
            purchase.TotalAmount = purchase.PurchaseItems
                 .Sum(i => i.SubTotal);

            _operationContext.Purchases.Add(purchase);

            await _operationContext.SaveChangesAsync();

            foreach(var item in purchase.PurchaseItems)
            {
                await _stockService.IncreaseStockAsync(item.ProductId, item.Quantity, purchase.PurchaseId);

            }

            return new PurchaseResponseDto
            {
                PurchaseId = purchase.PurchaseId,
                SupplierId = purchase.SupplierId,
                PurchaseDate = purchase.PurchaseDate,
                TotalAmount = purchase.TotalAmount,

                Items = purchase.PurchaseItems
                    .Select(i => new PurchaseItemResponseDto
                    {
                        PurchaseItemId = i.PurchaseItemId,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitCost = i.UnitCost,
                        SubTotal = i.SubTotal
                    })
                    .ToList()
            };

        }

        public async Task<List<PurchaseResponseDto>> GetPurchasesAsync()
        {
            var purchases = await _operationContext.Purchases
                .Select(p => new PurchaseResponseDto
                {
                    PurchaseId = p.PurchaseId,
                    SupplierId = p.SupplierId,
                    PurchaseDate = p.PurchaseDate,
                    TotalAmount = p.TotalAmount,

                    Items = p.PurchaseItems
                        .Select(i => new PurchaseItemResponseDto
                        {
                            PurchaseItemId = i.PurchaseItemId,
                            ProductId = i.ProductId,
                            Quantity = i.Quantity,
                            UnitCost = i.UnitCost,
                            SubTotal = i.SubTotal
                        })
                        .ToList()
                })
                .ToListAsync();
            return purchases;
        }

        public async Task<PurchaseResponseDto?> GetPurchaseByIdAsync(int purchaseId)
        {
            var purchase = await _operationContext.Purchases
                .Where(p => p.PurchaseId == purchaseId)
                .Select(p => new PurchaseResponseDto
                {
                    PurchaseId = p.PurchaseId,
                    SupplierId = p.SupplierId,
                    PurchaseDate = p.PurchaseDate,
                    TotalAmount = p.TotalAmount,

                    Items = p.PurchaseItems
                        .Select(i => new PurchaseItemResponseDto
                        {   
                            PurchaseItemId = i.PurchaseItemId,
                            ProductId = i.ProductId,
                            Quantity = i.Quantity,
                            UnitCost = i.UnitCost,
                            SubTotal = i.SubTotal
                        })
                        .ToList()

                })
                .FirstOrDefaultAsync();
            return purchase;
        }
    }
}
