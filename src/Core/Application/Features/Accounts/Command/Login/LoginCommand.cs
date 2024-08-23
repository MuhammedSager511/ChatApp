using Application.Presistence.Contracts;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Command.Login
{
    public class LoginCommand:IRequest<BaseCommonResponse>
    {
        public LoginDto LoginDto { get; set; }

        public LoginCommand(LoginDto loginDto)
        {
            LoginDto = loginDto;
        }
        class Handler : IRequestHandler<LoginCommand,BaseCommonResponse>
        {
            private readonly UserManager<AppUser> userManager;
            private readonly RoleManager<IdentityRole> roleManager;
            private readonly IMapper mapper;
            private readonly SignInManager<AppUser> signInManager;
            private readonly ITokenServices tokenServices;

            public Handler(UserManager<AppUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IMapper mapper,
                           SignInManager<AppUser> signInManager,
                           ITokenServices tokenServices
                           )
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
                this.mapper = mapper;
                this.signInManager = signInManager;
                this.tokenServices = tokenServices;
            }

            public async Task<BaseCommonResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                BaseCommonResponse res = new();

                try
                {
                    var user =await userManager.FindByNameAsync(request.LoginDto.UserName);
                    if (user is not null) 
                    {
                        var result=await signInManager.CheckPasswordSignInAsync(user , request.LoginDto.Password,false);
                        if (result != null && result.Succeeded)
                        {
                            res.IsSuccess = true;
                            res.Message = "Login Success";
                            res.Data = new
                            {
                                userName = user.UserName,
                                email = user.Email,
                                token =tokenServices.CreateToken(user)
                            };
                            return res;
                        }
                        res.IsSuccess = false;
                        res.Message = "unAuthorized";
                        return res;
                    }
                    res.IsSuccess = false;
                    res.Message = "notFound";
                    return res;
                }
                catch (Exception ex)
                {
                   
                    res.IsSuccess = false;
                    res.Message=ex.InnerException.Message;
                    return res;
                }
            }
        }
    }
}
