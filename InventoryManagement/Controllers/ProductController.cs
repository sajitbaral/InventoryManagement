using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.IService;
using InventoryManagement.Dto;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDto>>> GetProducts()         /*ActionResult is used to return different HTTP responses like 200 OK, 404 Not Found which an API might return. So ActionResult allows us to return those kinds of HTTP results.*/
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct(CreateProductDto dto)
        {
            var product = await _productService.CreateProductAsync(dto);
            return Ok(product);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto dto)    /* IActionResult allows us to return different HTTP responses as update is either true or false. hence if 2 responses are expected, then IActionresult */
        {
            var updated = await _productService.UpdateProductAsync(id, dto);
            if (!updated)               /* Since UpdateproductAsync is bool , updated is also bool. So If the update was not successful, then this prints */
            {
                return NotFound();
            }

            return NoContent();         /* NoContent() means "The update was successful, and there's no response body that I need to send back. This means, Product found → update → save → 204"*/
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);

            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
