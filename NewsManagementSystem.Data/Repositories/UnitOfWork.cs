using NewsManagementSystem.Core.Interfaces;
using NewsManagementSystem.Data.Context;

namespace NewsManagementSystem.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private INewsRepository _newsRepository;
        private ICategoryRepository _categoryRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public INewsRepository News
        {
            get
            {
                return _newsRepository ??= new NewsRepository(_context);
            }
        }

        public ICategoryRepository Categories
        {
            get
            {
                return _categoryRepository ??= new CategoryRepository(_context);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

