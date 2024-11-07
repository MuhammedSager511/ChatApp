using Application.Extentions;
using Application.Features.Accounts.Command.Register;
using Application.Features.Accounts.Queries.GetAllUsers;
using Application.Features.Message.Command.AddMessage;
using Application.Features.Message.Query.GetAllMessages;
using Application.Helper;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
    public class MappingProfile:Profile
    {
        private const string baseURL = "https://localhost:7002/";
        public MappingProfile()
        {
            //Mapping Message
            CreateMap<Message,AddMessageDto>().ReverseMap();
            CreateMap<Message,MessageReturnDto>().ReverseMap();

            ////mapping appuser-memberdto
            CreateMap<AppUser, MemberDto>()
                .ForMember(d => d.PhoneUrl, o => o.MapFrom(x => baseURL + x.Photos.FirstOrDefault(c => c.IsMain).Url))
                //.ForMember(d=>d.PhoneUrl,o=>o.MapFrom<UserMemberResolver>())
                .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))
                .ReverseMap();
    

            CreateMap<Photo,PhotoDto>()
                .ForMember(d=>d.Url,o=>o.MapFrom<UserPhotoResolver>())
                .ReverseMap();

            //mapping appuser-RegisterDto
            CreateMap<AppUser, RegisterDto>()
          .ReverseMap();

        }
    }
}
