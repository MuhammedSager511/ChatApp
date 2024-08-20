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
        }
    }
}
