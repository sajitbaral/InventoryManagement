using Inventory.Application.DTOs.Stock;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StockResponseDto>>> GetStocks(CancellationToken cancellationToken)
        {
            var stocks= await _stockService.GetStocksAsync(cancellationToken);
            return Ok(stocks);
        }

        [HttpGet ("{stockId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<StockResponseDto>> GetById(int stockId, CancellationToken cancellationToken)
        {
            var stock = await _stockService.GetByIdAsync(stockId, cancellationToken);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpGet("product/{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<StockResponseDto>> GetByProductId(int productId, CancellationToken cancellationToken)
        {
            var stock = await _stockService.GetByProductIdAsync(productId, cancellationToken);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost("increase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<StockResponseDto>> IncreaseStock(IncreaseStockDto dto,  CancellationToken cancellationToken)
        {
            var stock = await _stockService.IncreaseStockAsync(dto, cancellationToken);
            return Ok(stock);
        }

        [HttpPost("decrease")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StockResponseDto>> DecreaseStock(DecreaseStockDto dto, CancellationToken cancellationToken)
        {
            var stock = await _stockService.DecreaseStockAsync(dto, cancellationToken);
            return Ok(stock);
        }

        [HttpPost("adjust")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StockResponseDto>> AdjustStock(AdjustmentStockDto dto, CancellationToken cancellationToken)
        {
            var stock = await _stockService.AdjustmentStockAsync(dto, cancellationToken);
            return Ok(stock);
        }
    }
}
