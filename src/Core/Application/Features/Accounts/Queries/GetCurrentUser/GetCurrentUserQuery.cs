using Application.Presistence.Contracts;
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

namespace Application.Features.Accounts.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<UserReturnDto>
    {

        class Handler : IRequestHandler<GetCurrentUserQuery, UserReturnDto>
        {
            private readonly IHttpContextAccessor httpContext;
            private readonly UserManager<AppUser> userManager;
            private readonly ITokenServices token;

            public Handler(IHttpContextAccessor httpContext, UserManager<AppUser> userManager, ITokenServices token)
            {
                this.httpContext = httpContext;
                this.userManager = userManager;
                this.token = token;
            }
            public async Task<UserReturnDto> Handle(GetCurrentUserQuery request,
                                                    CancellationToken cancellationToken
                                                   )
            {
                try
                {
                    var userName = httpContext.HttpContext?.
                                          User.Claims.
                                          FirstOrDefault
                                          (x => x.Type == ClaimTypes.GivenName)?.Value;

                    if (userName is not null)
                    {
                        var user = await userManager.FindByNameAsync(userName);
                        return new UserReturnDto()
                        {
                            Email = user.Email,
                            UserName = user.UserName,
                            UserID = user.Id,
                            Token = token.CreateToken(user)

                        };
                    }
                    return null;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }
}
