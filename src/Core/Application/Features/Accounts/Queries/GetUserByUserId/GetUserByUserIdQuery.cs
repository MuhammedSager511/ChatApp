using Application.Features.Accounts.Queries.GetAllUsers;
using Application.Features.Accounts.Queries.GetUserByUserName;
using Application.Presistence.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Queries.GetUserByUserId
{
    public class GetUserByUserIdQuery:IRequest<MemberDto>
    {
        public string UserId { get; set; }
        public GetUserByUserIdQuery(string userId)
        {
            UserId = userId;
        }

        class Handler : IRequestHandler<GetUserByUserIdQuery, MemberDto>
        {
            private readonly IUserRepositry userRepositry;
            private readonly IMapper mapper;

            public Handler(IUserRepositry userRepositry, IMapper mapper)
            {
                this.userRepositry = userRepositry;
                this.mapper = mapper;

            }
            public async Task<MemberDto> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (!string.IsNullOrEmpty(request.UserId))
                    {
                        var user = await userRepositry.GetUserByIdAsync(request.UserId);
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
