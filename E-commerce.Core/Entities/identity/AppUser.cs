using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Entities.identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        //One user has many addresses => just for future :Icollection
        public Address Address { get; set; }
    }
}
