using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Command.CheckUserNameOrEmailExist
{
    public class CheckUserNameOrEmailExistQuery:IRequest<bool>
    {
        public CheckUserNameOrEmailExistQuery(string searchTerm)
        {
           this. SearchTerm = searchTerm;
           
        }

        public string SearchTerm { get; set; }




        class Handler : IRequestHandler<CheckUserNameOrEmailExistQuery, bool>
        {
            private readonly UserManager<AppUser> userManager;
         

            public Handler(UserManager<AppUser>userManager)
            {
                this.userManager = userManager;
                
            }

            public async Task<bool> Handle(CheckUserNameOrEmailExistQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {

                        if (request.SearchTerm.Contains("@"))
                        {
                            var email = await userManager.FindByEmailAsync(request.SearchTerm);
                            if (email != null)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            var userName = await userManager.FindByNameAsync(request.SearchTerm);
                            if (userName != null)
                            {
                                return true;
                            }
                        }

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
