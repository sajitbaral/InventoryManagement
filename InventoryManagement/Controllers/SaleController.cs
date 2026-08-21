using InventoryManagement.Dto;
using InventoryManagement.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;

        }

        [HttpPost]
        public async Task<ActionResult<SaleResponseDto>> CreateSale(CreateSaleDto dto)
        {
            var sale = await _saleService.CreateSaleAsync(dto);

            return Ok(sale);
        }

        [HttpGet]
        public async Task<ActionResult<List<SaleResponseDto>>> GetSales()
        {
            var sales = await _saleService.GetSalesAsync();

            return Ok(sales);
        }

        [HttpGet("{saleId}")]
        public async Task<ActionResult<SaleResponseDto>> GetSaleById(int saleId)
        {
            var sale = await _saleService.GetSaleByIdAsync(saleId);

            if (sale == null)
            {
                return NotFound("Sale not found");
            }

            return Ok(sale);

        }
    }
}
