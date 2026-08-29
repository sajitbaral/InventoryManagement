using InventoryManagement.Dto;
using InventoryManagement.Entities.Inventory;
using InventoryManagement.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
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
        public async Task<ActionResult<List<StockResponseDto>>> GetStocks()
        {
            var stocks = await _stockService.GetStocksAsync();

            return Ok(stocks);
        }

        [HttpGet("{stockId}")]
        public async Task<ActionResult<StockResponseDto>> GetStockById(int stockId)
        {
            var stock = await _stockService.GetStockByIdAsync(stockId);

            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

      

        [HttpPost("increase")]
        public async Task<ActionResult>IncreaseStock(int productId, int quantity, int purchaseId)
        {
            var stock = await _stockService.IncreaseStockAsync(productId, quantity, purchaseId);

            return Ok(stock);
        }

        [HttpPost("decrease")]
        public async Task<IActionResult> DecreaseStock(int productId, int quantity, int saleId)
        {
            var stock = await _stockService.DecreaseStockAsync(productId, quantity, saleId);
            return Ok(stock);
        }

        [HttpPost("adjust")]
        public async Task<IActionResult> AdjustStock( AdjustStockRequestDto request)
        {
            var stock = await _stockService.AdjustStockAsync(
                
                request.ProductId,
                request.Quantity,
                request.AdjustmentType
                );
            return Ok(stock);
        }
    }
}
