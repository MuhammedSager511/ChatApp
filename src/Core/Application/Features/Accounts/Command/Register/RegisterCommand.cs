using Application.Presistence.Contracts;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
            private readonly IMapper mapper;

            public Handler(UserManager<AppUser> userManager,ITokenServices tokenServices,IMapper mapper)
                          
            {
                this.userManager = userManager;
                this.tokenServices = tokenServices;
                this.mapper = mapper;
            }

            public async Task<BaseCommonResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                BaseCommonResponse res = new();

                try
                {
                    //Create validtor
                    var validator = new RegisterValidator();
                    var validatorResult=await validator.ValidateAsync(request.RegisterDto, cancellationToken);
                    if (validatorResult.IsValid ==false)
                    {
                        res.IsSuccess = false;
                        res.Message = "While Validator Register Data";
                        res.Erorrs=validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                        return res;
                        
                    }
                    var user=mapper.Map<AppUser>(request.RegisterDto);
                    //var user = new AppUser()
                    //{
                    //    Email = request.RegisterDto.Email,
                    //    UserName = request.RegisterDto.UserName,
                    //    City=request.RegisterDto.city,
                    //    Country = request.RegisterDto.country,
                    //    KnownAs=request.RegisterDto.KnownAs,
                    //    DateOfBirth=request.RegisterDto.DateOfBirth,
                    //    Gender=request.RegisterDto.Gender,
                    //};
                  var response=  await userManager.CreateAsync(user,request.RegisterDto.Password);
                  var roleResponse=   await userManager.AddToRoleAsync(user, "Member");
                    if (roleResponse.Succeeded == false)
                    {
                        foreach (var item in roleResponse.Errors)
                        {
                            res.Erorrs.Add($"{item.Code} - {item.Description}");
                        }
                        res.IsSuccess = false;
                        res.Message = "badRequst";
                        return res;

                    }
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
                        kuownAs=user.KnownAs,
                        //city=user.City,
                        //country=user.Country,
                        //dateofBirth=user.DateOfBirth,
                        gender=user.Gender,
                        token=await tokenServices.CreateToken(user),
                        photoUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.Url,
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
