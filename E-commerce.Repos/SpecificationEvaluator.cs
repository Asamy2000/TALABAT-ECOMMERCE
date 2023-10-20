using E_commerce.Core.Entities;
using E_commerce.Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repos
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        //IQueryable It allows you to write queries that are executed against the data source rather than fetching all data into memory
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery /*dbset*/, ISpecification<TEntity> spec)
        {
            var query = inputQuery; //_contect.Set<T>();

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria); //_contect.Set<T>().Where(condition);


            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);


            if(spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);


            //1- currentQuery = query  && IncludeExpression = spec.Includes[0]
            // query = query + spec.Includes[0]
            // 2- currentQuery = query & IncludeExpression = spec.Includes[1]
            //....
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return query;
        }

    }
}
