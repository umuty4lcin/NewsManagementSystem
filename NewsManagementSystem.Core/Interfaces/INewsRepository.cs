using NewsManagementSystem.Core.Entities;

namespace NewsManagementSystem.Core.Interfaces
{
    public interface INewsRepository : IRepository<News>
    {
        Task<IEnumerable<News>> GetPublishedNewsAsync();
        Task<IEnumerable<News>> GetNewsByCategoryAsync(int categoryId);
        Task<IEnumerable<News>> GetLatestNewsAsync(int count);
        Task<News> GetNewsByIdWithDetailsAsync(int id);
        Task IncrementViewCountAsync(int id);
    }
}

