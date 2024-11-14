using Application.Features.Accounts.Queries.GetAllUsers;
using Application.Features.Message.Command.AddMessage;
using Application.Features.Message.Query.GetAlMessageForUse;
using Application.Presistence.Contracts;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Command.GetMessageRead
{
    public class GetMessageReadCommand:IRequest<IEnumerable<MessageDto>>
    {
        public GetMessageReadCommand(string recipientUserName)
        {
            RecipientUserName = recipientUserName;
        }

        public string CurrentUserName { get; set; } 
        public string RecipientUserName { get; set; }

        class Handler : IRequestHandler<GetMessageReadCommand, IEnumerable<MessageDto>>
        {
            private readonly IMessageRepository messageRepository;
            private readonly IHttpContextAccessor httpContextAccessor;

            public Handler(IMessageRepository messageRepository,IHttpContextAccessor httpContextAccessor)
            {
                this.messageRepository = messageRepository;
                this.httpContextAccessor = httpContextAccessor;
            }
            public async Task<IEnumerable<MessageDto>> Handle(GetMessageReadCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    request.CurrentUserName = httpContextAccessor?.HttpContext?.User?.Claims?
                        .FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;

                    var message = await messageRepository.GetMessageRead(
                        request.CurrentUserName, request.RecipientUserName);
                    return message;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
