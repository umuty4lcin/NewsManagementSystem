using NewsManagementSystem.Core.Entities;

namespace NewsManagementSystem.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesWithNewsAsync();
    }
}

