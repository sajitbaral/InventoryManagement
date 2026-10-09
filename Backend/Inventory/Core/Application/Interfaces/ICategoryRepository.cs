using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken);
        Task<Category?> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken);
        void Add(Category category);
        void Delete(Category category);

    }
}
