using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Queries.GetUsersWithRole
{
    public class GetUsersWithRoleQuery:IRequest<List<UsersWithRoleDto>>
    {

        class Handler : IRequestHandler<GetUsersWithRoleQuery, List<UsersWithRoleDto>>
        {
            private readonly UserManager<AppUser> userManager;

            public Handler(UserManager<AppUser> userManager)
            {
                this.userManager = userManager;
            }
            public async Task<List<UsersWithRoleDto>> Handle(GetUsersWithRoleQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var users=await userManager.Users.ToListAsync();
                    var usersWithRole=new List<UsersWithRoleDto>(); 

                    foreach (var user in users)
                    {
                        var roles=await userManager.GetRolesAsync(user);
                        var userDto = new UsersWithRoleDto
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Roles = roles.ToList()
                        };
                        usersWithRole.Add(userDto);
                    }
                    return usersWithRole;
                    //var users = userManager.Users
                    //    .Select(x => new UsersWithRoleDto()
                    //    { 
                    //        Id= x.Id,   
                    //        UserName= x.UserName,
                    //        Roles=userManager.GetRolesAsync(x).Result
                    //    }).ToList();
                    //return users;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
