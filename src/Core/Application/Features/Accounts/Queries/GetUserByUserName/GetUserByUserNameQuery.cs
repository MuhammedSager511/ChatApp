using Application.Features.Accounts.Queries.GetAllUsers;
using Application.Presistence.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Queries.GetUserByUserName
{
    public class GetUserByUserNameQuery:IRequest<MemberDto>
    {
        public string UserName { get; set; }
        public GetUserByUserNameQuery( string userName)
        {
            UserName = userName;
        }

        class Handler : IRequestHandler<GetUserByUserNameQuery, MemberDto>
        {
            private readonly IUserRepositry userRepositry;
            private readonly IMapper mapper;

            public Handler(IUserRepositry userRepositry, IMapper mapper)
            {
                this.userRepositry = userRepositry;
                this.mapper = mapper;
               
            }
            public async Task<MemberDto> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (!string.IsNullOrEmpty(request.UserName))
                    {
                        var user= await userRepositry.GetUserByUserNameAsync(request.UserName); 
                        if (user is not null)
                        {
                           return mapper.Map<MemberDto>(user);
                        }
                    }
                    return null;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
