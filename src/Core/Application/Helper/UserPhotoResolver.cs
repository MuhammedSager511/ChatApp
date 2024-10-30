using Application.Features.Accounts.Queries.GetAllUsers;
using AutoMapper;
using AutoMapper.Execution;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class UserPhotoResolver : IValueResolver<Photo, PhotoDto, string>
    {
        private readonly IConfiguration config;

        public UserPhotoResolver(IConfiguration config)
        {
            this.config = config;
        }
        public string Resolve(Photo source, PhotoDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Url))
            {
                return config["ApiURL"] + source.Url;
            }
            return null;
        }
    }
}
