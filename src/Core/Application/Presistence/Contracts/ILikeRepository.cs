using Application.Features.Likes.Commands.AddLike;
using Application.Helper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presistence.Contracts
{
  public  interface ILikeRepository
    {
        Task<bool> AddLike(string LikedUserId, string SourceUserId);
        Task<PagedList<likeDto>> GetUserLikes(LikeParams likeParams);
        Task<UserLike> GetUserLike(string sourceUserId, string likeUserId);
        Task<AppUser> GetUserWithLike(string userId);

    }
}
