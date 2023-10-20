using E_commerce.Core.Entities;

namespace E_commerce.Core.Specification
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {

        //this ctor is used to get all products [no criteria]
        public ProductWithBrandAndTypeSpecification(productSpecparams specparams)
            :base(P =>
               (string.IsNullOrEmpty(specparams.SearchVal) || P.Name.ToLower().Contains(specparams.SearchVal)) &&
               (!specparams.BrandId.HasValue || P.ProductBrandId == specparams.BrandId.Value ) &&
               (!specparams.TypeId.HasValue || P.ProductTypeId == specparams.TypeId.Value)

            )
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.ProductType);

            AddOrderBy(p => p.Name);

            if (!string.IsNullOrEmpty(specparams.Sort))
            {
                switch (specparams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDsc":
                        AddOrderByDsc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            ApplyPagination(specparams.PageZize * (specparams.PageIndex - 1),specparams.PageZize);

        }

        // this ctor is used to get a specific product by its id 
        //p => p.Id == Id this expression is the criteria
        //will chain the parent ctor which take criteria
        public ProductWithBrandAndTypeSpecification(int Id) : base(p => p.Id == Id)
        {

            Includes.Add(p => p.Brand);
            Includes.Add(p => p.ProductType);

        }

    }
}
