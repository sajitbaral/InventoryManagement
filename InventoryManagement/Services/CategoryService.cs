using InventoryManagement.Data;
using InventoryManagement.Dto;
using InventoryManagement.Entities.Inventory;
using InventoryManagement.IService;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly InventoryDbContext _context;
        public CategoryService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryResponseDto>> GetCategoriesAsync()
        {
            return await _context.Categories
                .Select(c=> new CategoryResponseDto
                {
                    CategoryId= c.CategoryId,
                    Name= c.Name,
                    Description= c.Description,
                })
                .ToListAsync();
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c=> c.CategoryId==id)
                .Select(c=> new CategoryResponseDto
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    Description = c.Description,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync (CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };

        }

        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return false;
            }

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if(category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
