using Application.Presistence.Contracts;
using Azure.Core;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepositry : IUserRepositry
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly IWebHostEnvironment webHost;
        private readonly IHttpContextAccessor httpContext;

        public UserRepositry(ApplicationDbContext context,UserManager<AppUser> userManager,
                            IWebHostEnvironment webHost,IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.userManager = userManager;
            this.webHost = webHost;
            this.httpContext = httpContext;
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

        public async Task<bool>UploadPhoto(IFormFile file, string pathName)
        {
        
           
            if (file is not null)
            {
                
                    var userName = httpContext?.HttpContext?.User?.Claims?
                            .FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                    if (userName is not null)
                    {
                        var user = await userManager.FindByNameAsync(userName);
                        if (user is not null)
                        {
                            Photo photo = new Photo();
                          
                            photo.Url = PhotoManager._uploadPhoto(webHost, file, pathName);
                            photo.AppUserId = user.Id;
                            photo.IsActive = true;
                            await context.Photos.AddAsync(photo);
                            await context.SaveChangesAsync();
                            return true;
                    }
                    }

                
                //

            }
            return false;
        }
        public async Task<bool> RemovePhoto(int id)
        {
            var currandPhoto=await context.Photos.FirstOrDefaultAsync(x => x.Id == id); 
            if (currandPhoto is not null)
            {
                context.Photos.Remove(currandPhoto);    
                await context.SaveChangesAsync();
                PhotoManager._removePhoto(webHost, currandPhoto.Url);
                return true;
            }
           return false;
        }

        public async Task<bool> SetMainPhoto(int id)
        {
            var userName = httpContext?.HttpContext?.User?.Claims?
                            .FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;

            if (userName is not null)
            {
                var user = await userManager.FindByNameAsync(userName);
                if(user is not null)
                {
                    var currantPhotos = await context.Photos.Where(x => x.AppUserId == user.Id).ToListAsync();

                    foreach (var item in currantPhotos)
                    {
                        item.IsMain = false;
                        context.Photos.Update(item);
                        context.SaveChanges();
                    }
                }
                var currentPhoto = await context.Photos.FindAsync(id);
                if (currentPhoto is not null)
                {
                    if (currentPhoto.IsMain == false)
                    {
                        currentPhoto.IsMain = true;
                        context.Photos.Update(currentPhoto);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            return false ;
        }
    }
}
