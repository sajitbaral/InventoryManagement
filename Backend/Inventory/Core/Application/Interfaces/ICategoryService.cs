using Inventory.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface ICategoryService
    {
        Task <List<CategoryResponseDto>> GetCategoriesAsync(CancellationToken cancellationToken);
        Task<CategoryResponseDto?> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken);
        Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto, CancellationToken cancellationToken);
        Task<bool> UpdateCategoryAsync(int categoryId, CategoryUpdateDto dto, CancellationToken cancellationToken);
        Task<bool> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken);

    }
}
