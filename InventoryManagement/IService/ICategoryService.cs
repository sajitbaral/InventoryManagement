using InventoryManagement.Dto;

namespace InventoryManagement.IService
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetCategoriesAsync();
        Task<CategoryResponseDto?>GetCategoryByIdAsync(int categoryId);      /*allowed to be null if not found*/
        Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto);
        Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto);    /*allowed to be false if not found. It is like if category found update and if not then return false*/
        Task<bool> DeleteCategoryAsync(int id);
        
    }
}
