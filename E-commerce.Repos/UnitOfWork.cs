using E_commerce.Core;
using E_commerce.Core.Entities;
using E_commerce.Core.IRepositories;
using E_commerce.Repos.Data;
using System.Collections;

namespace E_commerce.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceContext _context;
        private Hashtable _repositories;

        public UnitOfWork(EcommerceContext context) => _context = context;
        public async Task<int> Complete()
         => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
          => await _context.DisposeAsync();

        public IGenericRepo<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
        {
            //initialize if first request
            if (_repositories == null)
                _repositories = new Hashtable();

            //Get the entity type
            var type = typeof(TEntity).Name;

            //if the repo is not exist create and add it to the hashTable
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepo<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            //return the repo [cast from obj to IGenericRepo<TEntity>]
            return _repositories[type] as IGenericRepo<TEntity>;
        }
    }
}
