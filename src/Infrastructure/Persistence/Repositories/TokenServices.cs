using Application.Presistence.Contracts;
using Domain.Entities;
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

namespace Persistence.Repositories
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<AppUser> userManager;
        private readonly SymmetricSecurityKey symmetricSecurityKey ;
        public TokenServices(IConfiguration configuration,UserManager<AppUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));
        }
        public async Task<string> CreateToken(AppUser user)
        {
            var claim = new List<Claim>() 
            { 
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),

            };
            var roles =await userManager.GetRolesAsync(user);
            claim.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));



            var creds = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256Signature);
            var tokenDescribtor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(10),
                Issuer = configuration["Token:Issuer"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescribtor);
            return tokenHandler.WriteToken(token);
        }
    }
}
