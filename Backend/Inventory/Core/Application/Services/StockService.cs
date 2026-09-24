using Inventory.Application.DTOs.Stock;
using Inventory.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockMovementRepository _stockMovementRepository;

        public StockService(IStockRepository stockRepository, IStockMovementRepository stockMovementRepository)
        {
            _stockRepository = stockRepository;
            _stockMovementRepository= stockMovementRepository;
        }

        /*public async Task<StockResponseDto> IncreaseStockAsync(IncreaseStockDto dto, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByProductIdAsync(dto.ProductId, cancellationToken);
            if (stock == null)
            {
                throw new Exception("There is no stock for this product");
            }

        }*/
    }
}
