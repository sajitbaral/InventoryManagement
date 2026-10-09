using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryDbContext _context;

        public CategoryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
                .ToListAsync(cancellationToken);

           return categories;
        }

        public async Task<Category?>GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId, cancellationToken);

            return category;
        }

        public void Add(Category category)
        {
           _context.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}
