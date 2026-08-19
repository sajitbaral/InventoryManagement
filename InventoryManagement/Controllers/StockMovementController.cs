using InventoryManagement.Dto;
using InventoryManagement.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementController : ControllerBase
    {
        private readonly IStockMovementService _stockMovementService;

        public StockMovementController(IStockMovementService stockMovementService)
        {
            _stockMovementService = stockMovementService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StockMovementResponseDto>>> GetStockMovements()
        {
            var movements = await _stockMovementService.GetStockMovementsAsync();
            return Ok(movements);
        }

        [HttpGet("{stockMovementId}")]
        public async Task<ActionResult<StockMovementResponseDto>> GetStockById(int stockMovementId)
        {
            var movement = await _stockMovementService.GetStockMovementByIdAsync(stockMovementId);

            if (movement == null)
            {
                return NotFound();
            }

            return Ok(movement);
        }

        [HttpPost]
        public async Task<ActionResult<StockMovementResponseDto>>CreateStockMovement(CreateStockMovementDto dto)
        {
            var movement = await _stockMovementService.CreateStockMovementAsync(dto);

            return Ok(movement);
        }
    }
}
