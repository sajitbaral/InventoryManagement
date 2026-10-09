using Inventory.Application.DTOs.Category;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;

namespace Inventory.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryResponseDto>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories= await _categoryRepository.GetCategoriesAsync(cancellationToken);
            
            return categories.Select(c => new CategoryResponseDto
            {
                CategoryId = c.CategoryId,
                Name= c.Name,
                Description= c.Description

            })
                .ToList();
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken);

            if (category == null)
            {
                return null;
            }

            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto, CancellationToken cancellationToken)
        {

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _categoryRepository.Add(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                Name= category.Name,
                Description = category.Description
            };
        } 

        public async Task<bool> UpdateCategoryAsync(int categoryId, CategoryUpdateDto dto, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken);

            if(category == null)
            {
                return false;
            }

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken);
            if (category == null)
            {
                return false;
            }
            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken) ;

            return true;
        }
    }
}
