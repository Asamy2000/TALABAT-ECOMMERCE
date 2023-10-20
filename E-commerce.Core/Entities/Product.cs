using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PicUrl { get; set; } = default!;
        public decimal Price { get; set; }
        //Foreign Key [ProductBrand table]
        public int ProductBrandId { get; set; }
        //Navigational prop [ONE] => relation between product and product brand [ONE2Many]
        public ProductBrand Brand { get; set; } = default!;
        //Foreign Key [ProductType table]
        public int ProductTypeId { get; set; } 
        //Navigational prop [ONE] => relation between product and product type [ONE2Many]
        public ProductType ProductType { get; set; } = default!;

    }
}
