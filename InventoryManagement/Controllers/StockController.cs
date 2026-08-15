using InventoryManagement.Dto;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<StockResponseDto>> GetStockById(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);

            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        [HttpPost]
        public async Task<ActionResult<StockResponseDto>> CreateStock(CreateStockDto dto)
        {
            var stock = await _stockService.CreateStockAsync(dto);

            return Ok(stock);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, UpdateStockDto dto)
        {
            var updated = await _stockService.UpdateStockAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var deleted = await _stockService.DeleteStockAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

    }
}
