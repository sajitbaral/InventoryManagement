using Operational.Application.Abstractions.Messaging;
using Operational.Application.DTOs.Inventory;
using Operational.Application.DTOs.Sale;
using Operational.Application.Interfaces;
using Operational.Domain.Entities;
using System.Text.Json;

namespace Operational.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IInventoryClient _inventoryClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOutboxWriter _outboxWriter;

        public SaleService(ISaleRepository saleRepository, ICustomerRepository customerRepository, IInventoryClient inventoryClient, IUnitOfWork unitOfWork, IOutboxWriter outboxWriter)
        {
            _saleRepository = saleRepository;
            _customerRepository = customerRepository;
            _inventoryClient = inventoryClient;
            _unitOfWork = unitOfWork;
            _outboxWriter = outboxWriter;
        }

        public async Task<SaleResponseDto> CreateSaleAsync(CreateSaleDto dto, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(dto.CustomerId, cancellationToken);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with the ID {dto.CustomerId} not found.");
            }

            if (!customer.IsActive)
            {
                throw new Exception($"Customer with the ID {dto.CustomerId} is currently inactive.");
            }

            var saleItems = new List<SaleItem>();

            foreach(var item in dto.Items)
            {
                var product = await _inventoryClient.GetProduct(new GetProductRequest
                {
                    ProductId = item.ProductId,

                }, cancellationToken);
                
                if(product == null)
                {
                    throw new Exception($"Product with ProductId {item.ProductId} not found.");
                }

                if (!product.IsActive)
                {
                    throw new Exception($"Product with the ID {item.ProductId} is currently inactive.");
                }

                var saleItem = new SaleItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    SubTotal = product.Price * item.Quantity,
                };
                saleItems.Add(saleItem);
            }

            var now = DateTime.UtcNow;
            var sale = new Sale
            {
                CustomerId = dto.CustomerId,
                SaleDate = now,
                CreatedAt = now,
                UpdatedAt = now,
                SaleItems = saleItems
            };

            sale.TotalAmount = saleItems.Sum(si => si.SubTotal);

            try
            {

                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                _saleRepository.Add(sale);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var message = new ReserveStockForSaleMessage(sale.SaleId, saleItems.Select(item => new ReserveStockForSaleItem
                (
                    item.ProductId,
                    item.Quantity
                ))
                .ToList()
                );

                var payload = JsonSerializer.Serialize(message, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                var outboxDraft = new OutboxMessageDraft(OutboxMessageTypes.ReserveStockForSale, payload);
                _outboxWriter.Add(outboxDraft);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(CancellationToken.None);
                throw;
                
            }

            return new SaleResponseDto
            {
                SaleId = sale.SaleId,
                CustomerId = sale.CustomerId,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                CreatedAt = sale.CreatedAt,
                UpdatedAt = sale.UpdatedAt,
                Items = sale.SaleItems.Select(si => new SaleItemResponseDto
                {
                    SaleItemId = si.SaleItemId,
                    ProductId = si.ProductId,
                    Quantity = si.Quantity,
                    UnitPrice = si.UnitPrice,
                    SubTotal = si.SubTotal
                })
                .ToList()
            };
            
        }

        public async Task<List<SaleResponseDto>> GetSalesAsync(CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetSalesAsync(cancellationToken);

            return sales.Select(s => new SaleResponseDto
            {
                SaleId = s.SaleId,
                CustomerId = s.CustomerId,
                SaleDate = s.SaleDate,
                TotalAmount = s.TotalAmount,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                Items = s.SaleItems.Select(si => new SaleItemResponseDto
                {
                    SaleItemId = si.SaleItemId,
                    ProductId = si.ProductId,
                    Quantity = si.Quantity,
                    UnitPrice = si.UnitPrice,
                    SubTotal = si.SubTotal
                })
                .ToList()
            })
                .ToList();
        }

        public async Task<SaleResponseDto?> GetSaleByIdAsync(int saleId, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(saleId, cancellationToken);
            if (sale == null)
            {
                return null;
            }

            return new SaleResponseDto
            {
                SaleId = sale.SaleId,
                CustomerId = sale.CustomerId,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                CreatedAt = sale.CreatedAt,
                UpdatedAt = sale.UpdatedAt,
                Items = sale.SaleItems.Select(si => new SaleItemResponseDto
                {
                    SaleItemId = si.SaleItemId,
                    ProductId = si.ProductId,
                    Quantity = si.Quantity,
                    UnitPrice = si.UnitPrice,
                    SubTotal = si.SubTotal
                })
                .ToList()
            };
        }
    }
}
