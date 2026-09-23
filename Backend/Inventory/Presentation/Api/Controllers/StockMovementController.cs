using Inventory.Application.DTOs.StockMovement;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StockMovementResponseDto>>> GetStockMovements(CancellationToken cancellationToken)
        {
            var stockMovements = await _stockMovementService.GetStockMovementsAsync(cancellationToken);
            return Ok(stockMovements);
        }

        [HttpGet("{stockMovementId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockMovementResponseDto>> GetByIdAsync(int stockMovementId, CancellationToken cancellationToken)
        {
            var stockMovement = await _stockMovementService.GetByIdAsync(stockMovementId, cancellationToken);

            if (stockMovement == null)
            {
                return NotFound();
            }

            return Ok(stockMovement);
        }

        [HttpGet ("product/{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StockMovementResponseDto>>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var stockMovement = await _stockMovementService.GetByProductIdAsync(productId, cancellationToken);

            return Ok(stockMovement);
        }
    }
}
