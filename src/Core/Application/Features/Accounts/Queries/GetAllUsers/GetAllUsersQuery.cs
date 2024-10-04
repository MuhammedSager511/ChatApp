using Application.Features.Accounts.Queries.GetCurrentUser;
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
    public class GetAllUsersQuery :IRequest<List<MemberDto>>
    {
        class Handler : IRequestHandler<GetAllUsersQuery, List<MemberDto>>
        {
            private readonly IUserRepositry userRepositry;
            private readonly IMapper mapper;

            public Handler(IUserRepositry userRepositry,IMapper mapper)
            {
                this.userRepositry = userRepositry;
                this.mapper = mapper;
            }
            public async Task<List<MemberDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var users = await userRepositry.GetUsersAsync();
                    var res=mapper.Map<List<MemberDto>>(users);
                    return res;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}