using Application.Features.Accounts.Queries.GetAllUsers;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class UserMemberResolver : IValueResolver<AppUser, MemberDto, string>
    {
        private readonly IConfiguration config;

        public UserMemberResolver(IConfiguration config)
        {
            this.config = config;
        }
        public string Resolve(AppUser source, MemberDto destination, string destMember, ResolutionContext context)
        {
           
                return config["ApiURL"] + source.Photos.FirstOrDefault(x=>x.IsMain &&x.IsActive)?.Url;
         
        }
    }
}
