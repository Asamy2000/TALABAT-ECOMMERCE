using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Specification
{
    public class ProductWithFiltirationForCountSpecs : BaseSpecification<Product>
    {
        public ProductWithFiltirationForCountSpecs(productSpecparams specparams)
             : base(P =>
               (string.IsNullOrEmpty(specparams.SearchVal) || P.Name.ToLower().Contains(specparams.SearchVal)) &&
               (!specparams.BrandId.HasValue || P.ProductBrandId == specparams.BrandId.Value) &&
               (!specparams.TypeId.HasValue || P.ProductTypeId == specparams.TypeId.Value)

            )
        {
            
        }
    }
}
