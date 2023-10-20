using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.IRepositories
{
    public interface IBasketRepo
    {
        Task<CustomerBasket?> GetBasketAsync(string basketid);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketid);
    }
}
