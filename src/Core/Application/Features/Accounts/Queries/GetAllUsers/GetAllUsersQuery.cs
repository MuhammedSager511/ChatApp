using Application.Features.Accounts.Queries.GetCurrentUser;
using Application.Helper;
using Application.Presistence.Contracts;
using AutoMapper;
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

namespace Application.Features.Accounts.Queries.GetAllUsers
{
    public class GetAllUsersQuery :IRequest<PagedList<MemberDto>>
    {
        public GetAllUsersQuery(UserParams userParams)
        {
            this.userParams = userParams;
        }

        public UserParams userParams {  get; set; } 
        class Handler : IRequestHandler<GetAllUsersQuery, PagedList<MemberDto>>
        {
            private readonly IUserRepositry userRepositry;
            private readonly IMapper mapper;

            public Handler(IUserRepositry userRepositry,IMapper mapper)
            {
                this.userRepositry = userRepositry;
                this.mapper = mapper;
            }
            public async Task<PagedList<MemberDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var users = await userRepositry.GetMemberAsync(request.userParams);
                    //var res=mapper.Map<List<MemberDto>>(users);
                    return users;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}