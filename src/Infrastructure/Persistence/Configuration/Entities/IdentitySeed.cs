using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration.Entities
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            //create role 
            if (!roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>()
                {
                   new IdentityRole(){Name = "Admin"},
                   new IdentityRole(){Name = "Moderator"},
                   new IdentityRole() {Name = "Member"},
                };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);

                }
            }


            if (!userManager.Users.Any())
            {
                //create User
                var user = new AppUser()
                {
                  
                    UserName="muhammed",
                    Email="muhammed@511.com",
                    Country="SY",
                    City="Azaz",
                    Gender="Male",
                    DateOfBirth=new DateTime(2003,10,20),
                    KnownAs="511",
                    EmailConfirmed=true,

                };
                await userManager.CreateAsync(user,"Muhammed@511");
                await userManager.AddToRoleAsync(user,"Admin");
            }
          
        }
    }
}
