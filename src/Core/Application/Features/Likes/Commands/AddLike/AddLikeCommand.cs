using Application.Presistence.Contracts;
using Application.Responses;
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

namespace Application.Features.Likes.Commands.AddLike
{
    public class AddLikeCommand:IRequest<BaseCommonResponse>
    {
        public AddLikeCommand(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }


        class Handler : IRequestHandler<AddLikeCommand, BaseCommonResponse>
        {
            private readonly ILikeRepository likeRepository;
            private readonly IHttpContextAccessor httpContextAccessor;
            private readonly UserManager<AppUser> userManager;

            public Handler(ILikeRepository likeRepository,IHttpContextAccessor httpContextAccessor,UserManager<AppUser> userManager)
            {
                this.likeRepository = likeRepository;
                this.httpContextAccessor = httpContextAccessor;
                this.userManager = userManager;
            }
            public async Task<BaseCommonResponse> Handle(AddLikeCommand request, CancellationToken cancellationToken)
            {

                BaseCommonResponse res = new();
                try
                {
                    if (!string.IsNullOrEmpty(request.UserName))
                    {

                        var currenUser = httpContextAccessor?.HttpContext?.User.Claims
                            .FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                        var sourceUser = await userManager.FindByNameAsync(currenUser);
                        var destUser=await userManager.FindByNameAsync(request.UserName);


                        if (sourceUser != null && destUser != null)
                        {

                            if (sourceUser.UserName == request.UserName)
                            {
                                res.IsSuccess=false;
                                res.Message = "Can't like yourself";
                                return res;
                            } 
                            var userLike= await likeRepository.GetUserLike(sourceUser.Id,destUser.Id);
                            if (userLike != null)
                            {
                                res.IsSuccess= false;
                                res.Message = "you alerdy liked user";
                                return res;
                            }
                        }
                        var result =await likeRepository.AddLike(destUser.Id,sourceUser.Id);
                        if(result)
                        {
                            res.IsSuccess = true;
                            res.Message = "Add Like Successfully";
                            return res;
                        }

                    }

                    res.IsSuccess = false;
                    res.Message = "user Name not fount";
                    return res;
                }
                catch (Exception Ex)
                {
                    res.IsSuccess = false;
                    res.Message = Ex.Message;
                    return res;
                    throw;
                }
            }
        }
    }
}
