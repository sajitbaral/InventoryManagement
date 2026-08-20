using InventoryManagement.Dto;
using InventoryManagement.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseResponseDto>> CreatePurchase(CreatePurchaseDto dto)
        {
            var purchase = await _purchaseService.CreatePurchaseAsync(dto);
            return Ok(purchase);

        }

        [HttpGet]
        public async Task<ActionResult<List<PurchaseResponseDto>>> GetPurchases()
        {
            var purchases = await _purchaseService.GetPurchasesAsync();
            return Ok(purchases);
        }

        [HttpGet("{purchaseId}")]
        public async Task<ActionResult<PurchaseResponseDto>>GetPurchaseById(int purchaseId)
        {
            var purchase = await _purchaseService.GetPurchaseByIdAsync(purchaseId);

            if (purchase == null)
            {
                return NotFound("Purchase not found");
            }

            return Ok(purchase);
        }
    }
}
