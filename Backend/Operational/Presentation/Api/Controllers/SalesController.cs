using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Operational.Application.DTOs.Sale;
using Operational.Application.Interfaces;

namespace Operational.Api.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SaleResponseDto>>> GetSales(CancellationToken cancellationToken)
        {
            var sales = await _saleService.GetSalesAsync(cancellationToken);
            return Ok(sales);
        }

        [HttpGet("{saleId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SaleResponseDto?>> GetSaleById(int saleId,  CancellationToken cancellationToken)
        {
            var sale = await _saleService.GetSaleByIdAsync(saleId, cancellationToken);
            if(sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SaleResponseDto>> CreateSale(CreateSaleDto dto, CancellationToken cancellationToken)
        {
            var sale = await _saleService.CreateSaleAsync(dto, cancellationToken);
            return CreatedAtAction(
                nameof(GetSaleById),
                new { saleId = sale.SaleId },
                sale);
        }
    }
}
