using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Specification
{
    public class productSpecparams
    {
        private const int MaxPageSize = 10;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        private int pageSize = 5;

        public int PageZize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        public int PageIndex { get; set;} = 1;

        private string searchVal;
        public string? SearchVal
        {
            get { return searchVal; }
            set { searchVal = value.ToLower(); }
        }

    }
}
