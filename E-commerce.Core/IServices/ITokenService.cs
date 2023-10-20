using E_commerce.Core.Entities.identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.IServices
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user ,UserManager<AppUser> manager);
    }
}
