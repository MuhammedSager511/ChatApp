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

namespace Application.Features.Accounts.Command.Register
{
    public class RegisterCommand : IRequest<BaseCommonResponse>
    {
        private readonly RegisterDto RegisterDto;

        public RegisterCommand(RegisterDto registerDto )
        {
            this.RegisterDto = registerDto;
        }
        class Handler : IRequestHandler<RegisterCommand, BaseCommonResponse>
        {
            private readonly UserManager<AppUser> userManager;
            private readonly ITokenServices tokenServices;

            public Handler(UserManager<AppUser> userManager,ITokenServices tokenServices)
                          
            {
                this.userManager = userManager;
                this.tokenServices = tokenServices;
            }

            public async Task<BaseCommonResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                BaseCommonResponse res = new();

                try
                {
                    var user = new AppUser()
                    {
                        Email = request.RegisterDto.Email,
                        UserName = request.RegisterDto.UserName,
                    };
                  var response=  await userManager.CreateAsync(user,request.RegisterDto.Password);

                    if (response.Succeeded==false) 
                    {
                        foreach (var item in response.Errors)
                        {
                            res.Erorrs.Add($"{item.Code} - {item.Description}");
                        }
                        res.IsSuccess=false;
                        res.Message = "badRequst";
                        return res;
                    }

                    res.IsSuccess = true;
                    res.Message = "Regisrte Success";
                    res.Data = new
                    {
                        userName = user.UserName,
                        email = user.Email,
                        token = tokenServices.CreateToken(user)
                    };
                    return res;
                }
                catch (Exception ex)
                {
                    res.IsSuccess = false;
                    res.Message = ex.Message;
                    return res;
                }
                   
            }
        }
    }
}
