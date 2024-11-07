using Application.Features.Accounts.Queries.GetAllUsers;
using Application.Helper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Presistence.Contracts
{
    public interface IUserRepositry
    {
        Task UpdateUser(AppUser user);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<AppUser> GetUserByUserNameAsync(string userName);

        Task<PhotoDto> UploadPhoto(IFormFile file, string pathName);
        Task<bool> RemovePhoto(int id);

        Task<bool> SetMainPhoto(int id);

        Task<PagedList<MemberDto>> GetMemberAsync(UserParams userParams);
    }
}
