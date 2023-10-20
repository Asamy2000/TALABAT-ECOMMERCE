using E_commerce.Core.Entities.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_commerce.Extensions
{
    public static class UserManagerExtention
    {
        public static async Task<AppUser?> FindUserAddressAsync(this UserManager<AppUser> userManager , ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email);
            return user;
        }
    }
}
