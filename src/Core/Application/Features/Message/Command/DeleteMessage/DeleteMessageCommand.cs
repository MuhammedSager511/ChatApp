using Application.Presistence.Contracts;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Command.DeleteMessage
{
    public class DeleteMessageCommand:IRequest<BaseCommonResponse>
    {
        public DeleteMessageCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        class Handler : IRequestHandler<DeleteMessageCommand, BaseCommonResponse>
        {
            private readonly IMessageRepository messageRepository;
            private readonly IHttpContextAccessor httpContextAccessor;

            public Handler(IMessageRepository messageRepository,IHttpContextAccessor httpContextAccessor)
            {
                this.messageRepository = messageRepository;
                this.httpContextAccessor = httpContextAccessor;
            }
            public async Task<BaseCommonResponse> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
            {
                BaseCommonResponse res = new();
                try
                {
                   var userName=httpContextAccessor?.HttpContext?.User?.Claims?
                                    .FirstOrDefault(x=>x.Type==ClaimTypes.GivenName)?.Value;
                    var message=await messageRepository.GetMessage(request.Id); 
                    if (message.Sender.UserName!=userName && message.Recipient.UserName!=userName)
                    {
                        res.IsSuccess = false;
                        res.Message = "Unauthorized";
                        return res;
                    }
                    if(message.Recipient.UserName ==userName)message.RecipientDeleted = true;
                    if(message.Sender.UserName ==userName)message.SenderDeleted = true;

                  await  messageRepository.UpdateAsync(message);

                    if (message.SenderDeleted && message.RecipientDeleted)
                        messageRepository.DeleteMessage(message);

                    res.Message = "ok";
                    res.IsSuccess = true;
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
