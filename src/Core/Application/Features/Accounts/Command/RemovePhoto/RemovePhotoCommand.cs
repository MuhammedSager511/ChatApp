using Application.Features.Accounts.Command.UploadPhoto;
using Application.Presistence.Contracts;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Command.RemovePhoto
{
    public class RemovePhotoCommand : IRequest<bool>
    {
        public RemovePhotoCommand(int id)
        {
            Id = id;
           
        }

        public int Id { get; set; }
       
    }

    class Handler : IRequestHandler<RemovePhotoCommand, bool>
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
        public async Task<bool> Handle(RemovePhotoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if ( request.Id>0)
                {
                    //remone

                   await userRepositry.RemovePhoto(request.Id);


                    return true;

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
