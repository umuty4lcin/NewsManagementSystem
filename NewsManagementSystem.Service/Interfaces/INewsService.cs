using NewsManagementSystem.Core.Entities;

namespace NewsManagementSystem.Service.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<IEnumerable<News>> GetPublishedNewsAsync();
        Task<IEnumerable<News>> GetNewsByCategoryAsync(int categoryId);
        Task<IEnumerable<News>> GetLatestNewsAsync(int count);
        Task<News> GetNewsByIdAsync(int id);
        Task<News> GetNewsByIdWithDetailsAsync(int id);
        Task AddNewsAsync(News news);
        Task UpdateNewsAsync(News news);
        Task DeleteNewsAsync(int id);
        Task PublishNewsAsync(int id);
        Task UnpublishNewsAsync(int id);
        Task IncrementViewCountAsync(int id);
    }
}

