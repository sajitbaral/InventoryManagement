using Inventory.Application.DTOs.Category;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<List<CategoryResponseDto>>> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetCategoriesAsync(cancellationToken);
            return Ok(categories);

        }

        [HttpGet("{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<CategoryResponseDto>>GetCategoryById(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId, cancellationToken);

            if (category == null)
            {
                return NotFound();
            }
            
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<CategoryResponseDto>> CreateCategory(CreateCategoryDto dto,  CancellationToken cancellationToken)
        {
            var category= await _categoryService.CreateCategoryAsync(dto, cancellationToken);

            return CreatedAtAction(nameof(GetCategoryById), new { categoryId = category.CategoryId }, category);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateCategory(int categoryId, CategoryUpdateDto dto, CancellationToken cancellationToken)
        {
            var updated= await _categoryService.UpdateCategoryAsync(categoryId, dto, cancellationToken);

            if (!updated)
            {
                return NotFound(new
                {
                    Message= $"Category with ID {categoryId} is not found "
                });
            }

            return NoContent();
        }

        [HttpDelete("{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteCategory(int categoryId, CancellationToken cancellationToken)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(categoryId, cancellationToken);

            if (!deleted)
            {
                return NotFound(new
                {
                    Message = $"Category with ID {categoryId} is not found"
                });
            }
            return NoContent();
        }

    } 

    
}
