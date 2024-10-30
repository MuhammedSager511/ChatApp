using Application.Presistence.Contracts;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Command.SetMainPhoto
{
    public class SetMainPhotoCommand:IRequest<bool>
    {
        public SetMainPhotoCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; } 
    }

    class Handler : IRequestHandler<SetMainPhotoCommand, bool>
    {
        private readonly IUserRepositry userRepositry;
        private readonly UserManager<AppUser> userManager;

        public Handler(IUserRepositry userRepositry,UserManager<AppUser> userManager)
        {
            this.userRepositry = userRepositry;
            this.userManager = userManager;
        }
        public async Task<bool> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id > 0)
                {
                   var res=await userRepositry.SetMainPhoto(request.Id);

                    if(res)
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
