using E_commerce.Core.Entities;
using E_commerce.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.IRepositories
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        #region oldWay
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetBYIdAsync(int id);
        Task<T> GetBYNameAsync(string name);
        #endregion

        //after specification design pattern
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);

        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
