using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Entities.Order_Aggregate
{
    public class DeliveryMethod: BaseEntity
    {
        public DeliveryMethod() { }

        public DeliveryMethod(string shortName, string description, decimal cost, string deliveryTime)
        {
            this.ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deliveryTime;
        }

        public string ShortName { get; set; } = default!; 
        public string Description { get; set;} = default!;
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; } = default!;

        //Icollection<Order> Orders
    }
}
