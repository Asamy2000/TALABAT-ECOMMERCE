using E_commerce.Core.Entities;
using E_commerce.Core.IRepositories;

namespace E_commerce.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepo<TEntity>? Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
