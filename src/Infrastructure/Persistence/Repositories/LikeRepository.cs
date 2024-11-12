using Application.Extentions;
using Application.Features.Likes.Commands.AddLike;
using Application.Helper;
using Application.Presistence.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration config;

        public LikeRepository(ApplicationDbContext context,
                                IHttpContextAccessor httpContextAccessor,
                                UserManager<AppUser> userManager,
                                IConfiguration config)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.config = config;
        }

        public async Task<bool> AddLike(string LikedUserId,string SourceUserId)
        {
           
               
                UserLike userLike = new()
                {
                    LikedUserId = LikedUserId,
                    SourceUserId= SourceUserId
                };
                       await context.UserLikes.AddAsync(userLike);
                        await context.SaveChangesAsync();
               return true;
            
          
        }

        public async Task<UserLike> GetUserLike(string sourceUserId, string likeUserId)
        
            =>await context.UserLikes.FindAsync(sourceUserId,likeUserId);
        

        public async Task<PagedList<likeDto>> GetUserLikes(LikeParams likeParams)
        {
            var users=context.Users.Include(x => x.Photos).OrderBy(x=>x.UserName).AsQueryable();
            var likes=context.UserLikes.AsQueryable();
            if (likeParams.Pridicate == "liked")
            {
                likes=likes.Where(x=>x.SourceUserId==likeParams.UserId);
                users=likes.Select(x=>x.LikedUser);
            }
            if (likeParams.Pridicate == "likedBy")
            {
                likes = likes.Where(x => x.LikedUserId == likeParams.UserId);
                users = likes.Select(x => x.SourceUser);
            }
            var likeUser= users.Select(x=>new likeDto
            {
                Id=x.Id,
                Age=x.DateOfBirth.CalculateAge(),
                City=x.City,
                KnownAS=x.KnownAs,
                UserName=x.UserName,
                PhotoUrl = config["ApiURL"] +x.Photos.FirstOrDefault(x => x.IsMain && x.IsActive).Url
            });
            return await PagedList<likeDto>.CreateAsync(likeUser, likeParams.PageNumber, likeParams.PageSize);
        }
            
        public async Task<AppUser> GetUserWithLike(string userId)
        {
            return await context.Users.Include(x => x.LikeUser).FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
