using E_commerce.Core.Entities;
using E_commerce.Core.IRepositories;
using E_commerce.Core.Specification;
using E_commerce.Repos.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Repos
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly EcommerceContext _ecommerceContext;

        public GenericRepo(EcommerceContext ecommerceContext)
        {
            _ecommerceContext = ecommerceContext;
        }

        #region oldWay
        public async Task<T> GetBYIdAsync(int id)
            => await _ecommerceContext.Set<T>().FindAsync(id);
        



        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Product))
            {
                return (IReadOnlyList<T>)await _ecommerceContext.products.Include(P => P.Brand).Include(P => P.ProductType).ToListAsync();
            }
            return await _ecommerceContext.Set<T>().ToListAsync();
        }
            
            
        
        #endregion

        //after specification design pattern

       

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
           => SpecificationEvaluator<T>.GetQuery(_ecommerceContext.Set<T>(), spec);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
          =>  await ApplySpecification(spec).ToListAsync();
        

        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
          => await ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task Add(T entity)
            =>await _ecommerceContext.Set<T>().AddAsync(entity);
        

        public void Update(T entity)
            =>  _ecommerceContext.Set<T>().Update(entity);
       

        public void Delete(T entity)
           => _ecommerceContext.Set<T>().Remove(entity);

        public async Task<T> GetBYNameAsync(string name)
        {
            if (typeof(T) == typeof(ProductBrand))
            {
                return (T)(object)await _ecommerceContext.productBrands
                    .SingleOrDefaultAsync(e => e.Name == name);
            }

            return null;
        }


    }
}
