using Operational.Application.DTOs.Inventory;
using Operational.Application.DTOs.Purchase;
using Operational.Application.Interfaces;
using Operational.Domain.Entities;

namespace Operational.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IInventoryClient _inventoryClient;
        private readonly IUnitOfWork _unitOfWork;
        public PurchaseService(IPurchaseRepository purchaseRepository, ISupplierRepository supplierRepository, IInventoryClient inventoryClient, IUnitOfWork unitOfWork)
        {
            _purchaseRepository = purchaseRepository;
            _supplierRepository = supplierRepository;
            _inventoryClient = inventoryClient;
            _unitOfWork = unitOfWork;
        }

        public async Task <PurchaseResponseDto> CreatePurchaseAsync(CreatePurchaseDto dto, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetSupplierByIdAsync(dto.SupplierId, cancellationToken);

            if(supplier == null)
            {
                throw new KeyNotFoundException($"Supplier with the ID {dto.SupplierId} not found.");
            }

            if(!supplier.IsActive)
            {
                throw new Exception($"Supplier with the ID {dto.SupplierId} is currently inactive.");
            }

            foreach (var item in dto.Items)
            {
                var product = await _inventoryClient.GetProduct(new GetProductRequest
                {
                    ProductId = item.ProductId
                }, cancellationToken);

                if (product == null)
                {
                    throw new Exception($"Product with ProductId {item.ProductId} not found.");
                }

                if (product.IsActive == false)
                {
                    throw new Exception($"Product with ProductId {item.ProductId} is currently inactive.");
                }

            }
            var now = DateTime.UtcNow;
            var purchase = new Purchase
            {
                SupplierId = dto.SupplierId,
                PurchaseDate = now,
                CreatedAt = now,
                UpdatedAt = now,
                PurchaseItems = dto.Items.Select(item=> new PurchaseItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitCost = item.UnitCost,
                    SubTotal = item.Quantity * item.UnitCost
                })
                .ToList()
            };

            purchase.TotalAmount = purchase.PurchaseItems.Sum(pi => pi.SubTotal);

            _purchaseRepository.Add(purchase);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            foreach(var item in purchase.PurchaseItems)
            {
                await _inventoryClient.IncreaseStock(new IncreaseStockRequest
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ReferenceId = purchase.PurchaseId
                }, cancellationToken);
            }

            return new PurchaseResponseDto
            {
                PurchaseId = purchase.PurchaseId,
                SupplierId = purchase.SupplierId,
                PurchaseDate = purchase.PurchaseDate,
                TotalAmount = purchase.TotalAmount,
                CreatedAt = purchase.CreatedAt,
                UpdatedAt = purchase.UpdatedAt,
                Items = purchase.PurchaseItems.Select(pi => new PurchaseItemResponseDto
                {
                    PurchaseItemId = pi.ProductId,
                    ProductId = pi.ProductId,
                    Quantity = pi.Quantity,
                    UnitCost = pi.UnitCost,
                    SubTotal = pi.SubTotal
                })
                .ToList()
            };
        }
    }
}






