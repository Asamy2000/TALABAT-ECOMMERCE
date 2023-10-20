using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Entities.Order_Aggregate
{
    public class ProductItemOrderd
    {
        public ProductItemOrderd(int productId, string productName, string picUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PicUrl = picUrl;
        }
        public ProductItemOrderd()
        {
            
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string PicUrl { get; set; }
    }
}
