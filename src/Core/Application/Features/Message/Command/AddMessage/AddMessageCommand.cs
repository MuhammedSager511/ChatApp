using Application.Features.Message.Validator;
using Application.Presistence.Contracts;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Command.AddMessage
{
    public class AddMessageCommand:IRequest<BaseCommonResponse>
    {
        public AddMessageDto AddMessageDto { get; set; }
        public AddMessageCommand( AddMessageDto addMessageDto)
        {
            AddMessageDto=addMessageDto;
        }


        class Handler : IRequestHandler<AddMessageCommand, BaseCommonResponse>
        {
            private readonly IMessageRepository messageRepository;
            private readonly IConfiguration config;
            private readonly IMapper mapper;
            private readonly IHttpContextAccessor httpContextAccessor;
            private readonly UserManager<AppUser> userManager;

            public Handler(IMessageRepository messageRepository,IConfiguration config,IMapper mapper
                            ,IHttpContextAccessor httpContextAccessor,UserManager<AppUser> userManager)
            {
                this.messageRepository = messageRepository;
                this.config = config;
                this.mapper = mapper;
                this.httpContextAccessor = httpContextAccessor;
                this.userManager = userManager;
            }
            public async Task<BaseCommonResponse> Handle(AddMessageCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    BaseCommonResponse response = new();
                    MessageValidator validations = new();
                    var validationResult = await validations.ValidateAsync(request.AddMessageDto);

                    if (!validationResult.IsValid)
                    {
                        response.IsSuccess= false;
                        response.Message = "While Adding New Message";
                        response.Erorrs=validationResult.Errors.Select(x=>x.ErrorMessage).ToList();
                    }

                    var message = mapper.Map<Domain.Entities.Message>(request.AddMessageDto);

                    message.SenderId = httpContextAccessor?.HttpContext?.User?.Claims?
                                            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                    

                    message.SenderUserName= httpContextAccessor?.HttpContext?.User?.Claims?
                                            .FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;

                    var recipient=await userManager.Users.Include(x => x.Photos)
                        .FirstOrDefaultAsync(x => x.UserName == message.RecipientUserName);


                    var sender = await userManager.Users.Include(x => x.Photos)
                        .FirstOrDefaultAsync(x => x.UserName == message.SenderUserName);
                    message.RecipientId = recipient.Id;
                    if (recipient == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "recipient not fount";
                        return response;
                    }


                    if (message.SenderUserName == request.AddMessageDto.RecipientUserName)
                    {
                        response.IsSuccess = false;
                        response.Message = "u can't send message to your self";
                        return response;
                    }

                    await messageRepository.AddAsync(message);
                    response.IsSuccess = true;
                    response.Message = "Success Adding New Message";
                    response.Data = new
                    {
                        Id = message.Id,
                        dateRead=message.DateRead,
                        messageSend=message.MessageSend,
                        SenderId = message.SenderId,
                        SenderUserName = message.SenderUserName,
                        SenderProfileUrl = config["ApiURL"] + sender.Photos.FirstOrDefault(x => x.IsMain && x.IsActive).Url,

                        RecipientId = message.RecipientId,
                        RecipientUserName = message.RecipientUserName,
                        RecipientProfileUrl = config["ApiURL"] + recipient.Photos.FirstOrDefault(x => x.IsMain && x.IsActive).Url,

                        Content = message.Content
                    };

                    return response;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

    }
}
