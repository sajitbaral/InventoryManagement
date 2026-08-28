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

        /*[HttpPost]
        public async Task<ActionResult<StockResponseDto>> CreateStock(CreateStockDto dto)
        {
            var stock = await _stockService.CreateStockAsync(dto);

            return Ok(stock);
        }

        [HttpPut("{stockId}")]
        public async Task<IActionResult> UpdateStock(int stockId, UpdateStockDto dto)
        {
            var updated = await _stockService.UpdateStockAsync(stockId, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{stockId}")]
        public async Task<IActionResult> DeleteStock(int stockId)
        {
            var deleted = await _stockService.DeleteStockAsync(stockId);

            if (!deleted)
                return NotFound();

            return NoContent();
        }*/

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
