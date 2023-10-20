using E_commerce.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Specification.OrderSpec
{
    public class OrderSpecification: BaseSpecification<Order>
    {
        public OrderSpecification(string email) :base(O => O.BuyerEmail == email)
        {
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);

            AddOrderByDsc(O => O.OrderDate);
        }


        public OrderSpecification(string email , int orderId):base( O => O.BuyerEmail == email  && O.Id == orderId)
        {
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);

            
        }
    }
}
