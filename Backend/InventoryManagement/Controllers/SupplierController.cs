using InventoryManagement.Dto;
using InventoryManagement.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpPost]
        public async Task<ActionResult<SupplierResponseDto>> CreateSupplier(CreateSupplierDto dto)
        {
            var supplier = await _supplierService.CreateSupplierAsync(dto);
            return Ok(supplier);
        }

        [HttpGet]
        public async Task<ActionResult<SupplierResponseDto>> GetSuppliers()
        {
            var suppliers = await _supplierService.GetSupplierAsync();
            return Ok(suppliers);
        }

        [HttpGet("{supplierId}")]
        public async Task<ActionResult<SupplierResponseDto>> GetSupplierById(int supplierId)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(supplierId);

            if (supplier == null)
            {
                throw new Exception("Supplier not found.");
            }

            return Ok(supplier);
        }


    }
}
