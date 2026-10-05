using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Operational.Application.DTOs.Supplier;
using Operational.Application.Interfaces;

namespace Operational.Api.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SupplierResponseDto>>> GetSuppliers(
            CancellationToken cancellationToken)
        {
            var suppliers = await _supplierService
                .GetSuppliersAsync(cancellationToken);

            return Ok(suppliers);
        }

        [HttpGet("{supplierId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SupplierResponseDto>> GetSupplierById(
            int supplierId,
            CancellationToken cancellationToken)
        {
            var supplier = await _supplierService
                .GetSupplierByIdAsync(supplierId, cancellationToken);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SupplierResponseDto>> CreateSupplier(
            CreateSupplierDto dto,
            CancellationToken cancellationToken)
        {
            var supplier = await _supplierService
                .CreateSupplierAsync(dto, cancellationToken);

            return CreatedAtAction(
                nameof(GetSupplierById),
                new { supplierId = supplier.SupplierId },
                supplier);
        }

        [HttpPut("{supplierId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSupplier(
            int supplierId,
            UpdateSupplierDto dto,
            CancellationToken cancellationToken)
        {
            var updated = await _supplierService
                .UpdateSupplierAsync(
                    supplierId,
                    dto,
                    cancellationToken);

            if (!updated)
            {
                return NotFound(new
                {
                    Message = $"Supplier with SupplierId {supplierId} not found."
                });
            }

            return NoContent();
        }

        [HttpPatch("{supplierId:int}/deactivate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeactivateSupplier(
            int supplierId,
            CancellationToken cancellationToken)
        {
            var deactivated = await _supplierService
                .DeactivateSupplierAsync(
                    supplierId,
                    cancellationToken);

            if (!deactivated)
            {
                return NotFound(new
                {
                    Message = $"Supplier with SupplierId {supplierId} not found."
                });
            }

            return NoContent();
        }

        [HttpPatch("{supplierId:int}/activate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActivateSupplier(
            int supplierId,
            CancellationToken cancellationToken)
        {
            var activated = await _supplierService
                .ActivateSupplierAsync(supplierId, cancellationToken);

            if (!activated)
            {
                return NotFound(new
                {
                    Message = $"Supplier with SupplierId {supplierId} not found."
                });
            }

            return NoContent();
        }
    }
}
