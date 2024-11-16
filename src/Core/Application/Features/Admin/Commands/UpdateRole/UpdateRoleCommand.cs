using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Commands.UpdateRole
{
    public class UpdateRoleCommand:IRequest<BaseCommonResponse>
    {
        public UpdateRoleCommand(string userName, string roles)
        {
            UserName = userName;
            Roles = roles;
        }

        public string UserName { get; set; }
        public string Roles { get; set; }


        class Handler : IRequestHandler<UpdateRoleCommand, BaseCommonResponse>
        {
            private readonly UserManager<AppUser> userManager;

            public Handler(UserManager<AppUser> userManager)
            {
                this.userManager = userManager;
            }
            public async Task<BaseCommonResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
            {
                BaseCommonResponse response = new();
                try
                {
                    //Admin Member
                    var selectedRoles=request.Roles.Split(",").ToArray();
                    var user=await userManager.FindByNameAsync(request.UserName);
                    if (user!=null)
                    {
                        var userRples=await userManager.GetRolesAsync(user);
                        var result=await userManager.AddToRolesAsync(user, selectedRoles.Except(userRples));

                        if(!result.Succeeded)
                        {
                            response.IsSuccess=false;
                            response.Message = "badRequst";
                            response.Erorrs.Add("failed adding this role");
                            return response;
                        }
                        result=await userManager.RemoveFromRolesAsync(user,userRples.Except(selectedRoles));

                        if (!result.Succeeded)
                        {
                            response.IsSuccess = false;
                            response.Message = "badRequst";
                            response.Erorrs.Add("failed removing this role");
                            return response;
                        }

                    }
                    response.IsSuccess = true;
                    response.Message = "ok";
                    response.Data = await userManager.GetRolesAsync(user);
                    return response;


                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "badRequst";
                    response.Erorrs.Add(ex.Message);
                    return response;
                }
            }
        }
    }
}
