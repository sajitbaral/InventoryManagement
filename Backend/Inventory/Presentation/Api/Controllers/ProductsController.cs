using Inventory.Application.DTOs.Product;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductResponseDto>>> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsAsync(cancellationToken);
            return Ok(products);
        }

        [HttpGet("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductResponseDto>> GetProductById(int productId, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(productId, cancellationToken);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ProductResponseDto>> CreateProduct(CreateProductDto dto, CancellationToken cancellationToken)
        {
            var product = await _productService.CreateProductAsync(dto, cancellationToken);

            return CreatedAtAction(
                nameof(GetProductById),                 //The endpoint that can retrieve this newly created product is GetProductById.
                new { productId = product.ProductId },  //Which Product?
                product);                               //The newly created product is returned in the response body.

        }

        [HttpPut("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult>UpdateProduct(int productId, ProductUpdateDto dto, CancellationToken cancellationToken)
        {
            var updated= await _productService.UpdateProductAsync(productId, dto, cancellationToken);

            if (!updated)
            {
                return NotFound(new { 
                    Message = $"Product with ID {productId} is not found" });
            }

            return NoContent();
        }

        [HttpPatch("{productId:int}/deactivate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult>DeactivateProduct(int productId, CancellationToken cancellationToken)
        {
            var deactivated = await _productService.DeactivateProductAsync(productId, cancellationToken);

            if (!deactivated)
            {
                return NotFound(new {
                    Message= $"Product with ID{productId} is not found" 
                }); 
            }
            return NoContent();
        }

        [HttpPatch("{productId:int}/activate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActivateProduct(int productId, CancellationToken cancellationToken)
        {
            var activated = await _productService.ActivateProductAsync(productId, cancellationToken);

            if (!activated)
            {
                return NotFound(new
                {
                    Message = $"Customer with ProductId {productId} not found."
                });
            }

            return NoContent();
        }


    }
}
