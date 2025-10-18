namespace NewsManagementSystem.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        INewsRepository News { get; }
        ICategoryRepository Categories { get; }
        Task<int> SaveChangesAsync();
    }
}

