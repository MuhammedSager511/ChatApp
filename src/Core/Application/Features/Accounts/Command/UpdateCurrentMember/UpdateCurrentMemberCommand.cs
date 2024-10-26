using Application.Features.Accounts.Command.Register;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Command.UpdateCurrentMember
{
    public class UpdateCurrentMemberCommand:IRequest<BaseCommonResponse>
    {
        private readonly UpdateCurrentMemberDto updateCurrentMemberDto;

        public UpdateCurrentMemberCommand(UpdateCurrentMemberDto updateCurrentMemberDto)
        {
            this.updateCurrentMemberDto = updateCurrentMemberDto;
        }

        class Handler : IRequestHandler<UpdateCurrentMemberCommand, BaseCommonResponse>
        {
            private readonly UserManager<AppUser> userManager;
            private readonly IMapper mapper;
            private readonly IHttpContextAccessor httpContext;

            public Handler(UserManager<AppUser> userManager,IMapper mapper,IHttpContextAccessor httpContext)
            {
                this.userManager = userManager;
                this.mapper = mapper;
                this.httpContext = httpContext;
            }
            public async Task<BaseCommonResponse> Handle(UpdateCurrentMemberCommand request, CancellationToken cancellationToken)
            {
                BaseCommonResponse response= new();
                try
                {
                    var userName=httpContext?.HttpContext?.User?.Claims?
                                .FirstOrDefault(x=>x.Type==ClaimTypes.GivenName)?.Value;
                    if (userName is not null) 
                    { 
                        var currentUser=await userManager.FindByNameAsync(userName);
                        currentUser.Introduction = request.updateCurrentMemberDto.Introduction;
                        currentUser.LookingFor = request.updateCurrentMemberDto.LookingFor;
                        currentUser.City = request.updateCurrentMemberDto.City;
                        currentUser.Country = request.updateCurrentMemberDto.Country;
                        currentUser.Interests = request.updateCurrentMemberDto.Interests;

                        var res = await userManager.UpdateAsync(currentUser);

                        if (res.Succeeded) 
                        {
                            response.IsSuccess = true;
                            response.Message = "Update Currend User Successfuly!!";
                            response.Data=request.updateCurrentMemberDto;
                            return response;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            foreach (var err in res.Errors)
                            {
                                response.Erorrs.Add($"code {err.Code} - description {err.Description}");
                            }
                            response.Message = "badRequest";
                            return response;
                        }
                    }
                    response.IsSuccess = false;
                    response.Message = "User Name Incorrect!!";
                    return response;
                }
                catch (Exception ex)
                {

                    response.IsSuccess = false;
                    response.Message = ex.Message;
                    return response;
                }
            }
        }
    }
}
