using Application.Extentions;
using Application.Features.Accounts.Queries.GetAllUsers;
using Application.Features.Message.Command.AddMessage;
using Application.Features.Message.Query.GetAllMessages;
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
        public MappingProfile()
        {
            //Mapping Message
            CreateMap<Message,AddMessageDto>().ReverseMap();
            CreateMap<Message,MessageReturnDto>().ReverseMap();

            //mapping appuser-memberdto
            CreateMap<AppUser,MemberDto>()
                .ForMember(d=>d.PhoneUrl,o=>o.MapFrom(s=>s.Photos.FirstOrDefault(x=>x.IsMain).Url))
                .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))

                .ReverseMap();
            CreateMap<Photo,PhotoDto>().ReverseMap();
        }
    }
}
