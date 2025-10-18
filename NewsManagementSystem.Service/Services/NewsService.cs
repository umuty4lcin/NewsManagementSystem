using NewsManagementSystem.Core.Entities;
using NewsManagementSystem.Core.Interfaces;
using NewsManagementSystem.Service.Interfaces;

namespace NewsManagementSystem.Service.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _unitOfWork.News.GetAllAsync();
        }

        public async Task<IEnumerable<News>> GetPublishedNewsAsync()
        {
            return await _unitOfWork.News.GetPublishedNewsAsync();
        }

        public async Task<IEnumerable<News>> GetNewsByCategoryAsync(int categoryId)
        {
            return await _unitOfWork.News.GetNewsByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<News>> GetLatestNewsAsync(int count)
        {
            return await _unitOfWork.News.GetLatestNewsAsync(count);
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
            return await _unitOfWork.News.GetByIdAsync(id);
        }

        public async Task<News> GetNewsByIdWithDetailsAsync(int id)
        {
            return await _unitOfWork.News.GetNewsByIdWithDetailsAsync(id);
        }

        public async Task AddNewsAsync(News news)
        {
            await _unitOfWork.News.AddAsync(news);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateNewsAsync(News news)
        {
            await _unitOfWork.News.UpdateAsync(news);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteNewsAsync(int id)
        {
            await _unitOfWork.News.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task PublishNewsAsync(int id)
        {
            var news = await _unitOfWork.News.GetByIdAsync(id);
            if (news != null)
            {
                news.IsPublished = true;
                news.PublishedDate = DateTime.Now;
                await _unitOfWork.News.UpdateAsync(news);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UnpublishNewsAsync(int id)
        {
            var news = await _unitOfWork.News.GetByIdAsync(id);
            if (news != null)
            {
                news.IsPublished = false;
                await _unitOfWork.News.UpdateAsync(news);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task IncrementViewCountAsync(int id)
        {
            await _unitOfWork.News.IncrementViewCountAsync(id);
        }
    }
}

