using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        //pagination
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteriaEx)
        {
            Criteria = criteriaEx;
            
        }


        public void AddOrderBy(Expression<Func<T, object>> orderByEx)
        {
            OrderBy = orderByEx;
        }


        public void AddOrderByDsc(Expression<Func<T, object>> orderByDscEx)
        {
            OrderByDesc = orderByDscEx;
        }

        public void ApplyPagination(int skip ,int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
