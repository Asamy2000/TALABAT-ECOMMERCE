using E_commerce.Core.Entities.identity;
using E_commerce.Core.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            
            #region private-claims
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email),
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            } 
            #endregion

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"]));
            var token = new JwtSecurityToken(

                issuer: _configuration["jwt:Validissure"],
                audience: _configuration["jwt:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["jwt:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );

           return new JwtSecurityTokenHandler().WriteToken(token);
            }
    }
}
