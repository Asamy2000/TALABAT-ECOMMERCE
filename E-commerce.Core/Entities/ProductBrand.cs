using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Entities
{
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; } = default!;
        public ProductBrand(string name)
        {
            Name = name;
        }
        public ProductBrand()
        {
            
        }
        //Navigational prop One if buiesness needs [configured using fluentApi]
    }
}
