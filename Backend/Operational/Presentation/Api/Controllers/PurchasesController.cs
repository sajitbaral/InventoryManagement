using Microsoft.AspNetCore.Mvc;
using Operational.Application.DTOs.Purchase;
using Operational.Application.Interfaces;

namespace Operational.Api.Controllers
{
    [Route("api/purchases")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PurchaseResponseDto>>> GetPurchases(CancellationToken cancellationToken)
        {
            var purchases = await _purchaseService.GetPurchasesAsync(cancellationToken);
            return Ok(purchases);
        }

        [HttpGet("{purchaseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PurchaseResponseDto?>> GetPurchaseById(int purchaseId, CancellationToken cancellationToken)
        {
            var purchase = await _purchaseService.GetPurchaseByIdAsync(purchaseId, cancellationToken);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PurchaseResponseDto>> CreatePurchase(CreatePurchaseDto dto,  CancellationToken cancellationToken)
        {
            var purchase = await _purchaseService.CreatePurchaseAsync(dto, cancellationToken);
            return CreatedAtAction(
                nameof(GetPurchaseById),
                new { purchaseId = purchase.PurchaseId },
                purchase);
        }
    }
}
