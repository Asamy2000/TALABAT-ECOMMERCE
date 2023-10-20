using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Entities
{
    public class BasketItem 
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal price { get; set; }
        public string pictureUrl { get; set; } = default!;
        public int quantity { get; set; }
        public string brand { get; set; } = default!;
        public string type { get; set; } = default!;
    }
}
