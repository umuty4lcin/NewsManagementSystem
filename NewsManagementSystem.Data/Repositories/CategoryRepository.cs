using Microsoft.EntityFrameworkCore;
using NewsManagementSystem.Core.Entities;
using NewsManagementSystem.Core.Interfaces;
using NewsManagementSystem.Data.Context;

namespace NewsManagementSystem.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithNewsAsync()
        {
            return await _dbSet
                .Include(c => c.NewsList.Where(n => n.IsActive && n.IsPublished))
                .Where(c => c.IsActive)
                .ToListAsync();
        }
    }
}

