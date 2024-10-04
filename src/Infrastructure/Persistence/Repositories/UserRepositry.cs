using Application.Presistence.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepositry : IUserRepositry
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;

        public UserRepositry(ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<AppUser> GetUserByIdAsync(string userId)
            => await userManager.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == userId);



        public async Task<AppUser> GetUserByUserNameAsync(string userName)
        => await userManager.Users.Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.UserName == userName);

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        => await userManager.Users.Include(x=>x.Photos).ToListAsync();

        public void UpdateUser(AppUser user)
        {
            userManager.UpdateAsync(user);
            context.SaveChanges();
        }
    }
}
