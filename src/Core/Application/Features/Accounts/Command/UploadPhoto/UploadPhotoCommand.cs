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

namespace Application.Features.Accounts.Command.UploadPhoto
{
    public class UploadPhotoCommand:IRequest<bool>
    {
        public IFormFile PhotoFile { get; set; }

        class Handler : IRequestHandler<UploadPhotoCommand, bool>
        {
            private readonly IUserRepositry userRepositry;
            private readonly IHttpContextAccessor httpContext;
            private readonly UserManager<AppUser> userManager;

            public Handler(IUserRepositry userRepositry, IHttpContextAccessor httpContext,
                           UserManager<AppUser> userManager)
            {
                this.userRepositry = userRepositry;
                this.httpContext = httpContext;
                this.userManager = userManager;
            } 
            public async Task<bool> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.PhotoFile is not null)
                    {
                      var result= await userRepositry.UploadPhoto(request.PhotoFile, "User");
                        if (result)
                        {
                            return true;
                        }
                        
                        return false;
                   
                    }
                    return false;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
    }
}
