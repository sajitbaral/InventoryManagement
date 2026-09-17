using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken);
        Task<Category?> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken);
        Task AddAsync(Category category);
        void Delete(Category category);
        Task SaveChangesAsync(CancellationToken cancellationToken);

    }
}
