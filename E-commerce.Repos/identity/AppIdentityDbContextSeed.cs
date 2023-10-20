using E_commerce.Core.Entities.identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Repos.identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Ahmed samy",
                    Email = "A.samy@gmail.com",
                    UserName = "A.samy",
                    PhoneNumber = "1234567890",
                };
                await userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }

    }
}
