using Microsoft.EntityFrameworkCore;
using NewsManagementSystem.Core.Entities;
using NewsManagementSystem.Core.Interfaces;
using NewsManagementSystem.Data.Context;

namespace NewsManagementSystem.Data.Repositories
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<News>> GetPublishedNewsAsync()
        {
            return await _dbSet
                .Include(n => n.Category)
                .Include(n => n.User)
                .Where(n => n.IsActive && n.IsPublished)
                .OrderByDescending(n => n.PublishedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetNewsByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Include(n => n.Category)
                .Include(n => n.User)
                .Where(n => n.IsActive && n.IsPublished && n.CategoryId == categoryId)
                .OrderByDescending(n => n.PublishedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetLatestNewsAsync(int count)
        {
            return await _dbSet
                .Include(n => n.Category)
                .Include(n => n.User)
                .Where(n => n.IsActive && n.IsPublished)
                .OrderByDescending(n => n.PublishedDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<News> GetNewsByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(n => n.Category)
                .Include(n => n.User)
                .FirstOrDefaultAsync(n => n.Id == id && n.IsActive);
        }

        public async Task IncrementViewCountAsync(int id)
        {
            var news = await GetByIdAsync(id);
            if (news != null)
            {
                news.ViewCount++;
                await _context.SaveChangesAsync();
            }
        }
    }
}

