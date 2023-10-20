using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Specification
{
    //the specification interface which include signeture of query component
    public interface ISpecification<T> where T : BaseEntity
    {
        //Criteria => value of the where condition
        public Expression<Func<T, bool>> Criteria { get; set; }
       
        //Includes signature [entity may have one navigational prop or more]
        public List<Expression<Func<T,object>>> Includes { get; set; }
        
        //orderBy signature
        public Expression<Func<T,object>> OrderBy { get; set; }
        public Expression<Func<T,object>> OrderByDesc { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
