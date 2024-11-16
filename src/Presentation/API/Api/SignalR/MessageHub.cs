using Application.Features.Message.Command.AddMessage;
using Application.Presistence.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Api.SignalR
{
    public class MessageHub:Hub
    {
        private readonly IMessageRepository messageRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public MessageHub(IMessageRepository messageRepository, IMediator mediator,IMapper mapper,UserManager<AppUser> userManager)
        {
            this.messageRepository = messageRepository;
            this.mediator = mediator;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public override async Task OnConnectedAsync()
        {
            var httpContext=Context.GetHttpContext();
            var otherUser = httpContext?.Request?.Query["user"].ToString(); 
            var currentUserName = Context?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;

            var groupName = GetGroupName(currentUserName, otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var message = await messageRepository.GetMessageRead(currentUserName, otherUser);
            await Clients.Group(groupName).SendAsync("ReceivedMessageRead", message);
        }
        private string GetGroupName(string caller,string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" :$"{other}-{caller}";
        }


        //send message
        public async Task SendMessage(AddMessageDto addMessageDto)
        {
            var message=mapper.Map<Message>(addMessageDto);

            
            message.SenderId= Context?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        
            message.SenderUserName= Context?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value??"";


            var recipient=await userManager.Users.Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.UserName==message.RecipientUserName);

            var sender = await userManager.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.UserName == message.SenderUserName);

            message.RecipientId = recipient.Id;
            await messageRepository.AddAsync(message);
            var caller = sender.UserName;
            var other = recipient.UserName;
            var groupName = GetGroupName(caller, other);
            await Clients.Group(groupName).SendAsync("NewMessage", message);



        }




        //public async Task SendMessage(AddMessageDto addMessageDto)
        //{
        //    var command = new AddMessageCommand(addMessageDto);
        //    var response = await mediator.Send(command);

        //    if (response.IsSuccess==false) throw new HubException(response.Message);    




        //    dynamic dataObject= JsonConvert.DeserializeObject(response.Data.ToString());

        //    var caller = dataObject.SenderUserName;
        //    var other = dataObject.SenderUserName;
        //    var groupName = GetGroupName(caller, other);
        //    await Clients.Group(groupName).SendAsync("NewMessage", response.Data);
        //}
        //return response.IsSuccess ? Ok(response.Data) : BadRequest(response.Message);




   
         public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (exception != null)
            {
                Console.WriteLine("Disconnected with error: " + exception.Message);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
