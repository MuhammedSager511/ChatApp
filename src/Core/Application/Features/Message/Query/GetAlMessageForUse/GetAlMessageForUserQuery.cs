using Application.Features.Message.Query.GetAllMessages;
using Application.Helper;
using Application.Presistence.Contracts;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Query.GetAlMessageForUse
{
    public class GetAlMessageForUserQuery:IRequest<PagedList<MessageDto>>
    {
        public GetAlMessageForUserQuery(MessageParams messageParams)
        {
            MessageParams = messageParams;
        }

        public MessageParams MessageParams { get; set; }

        class Handler : IRequestHandler<GetAlMessageForUserQuery, PagedList<MessageDto>>
        {
            private readonly IMessageRepository messageRepository;
            private readonly IHttpContextAccessor httpContextAccessor;

            public Handler(IMessageRepository messageRepository, IHttpContextAccessor httpContextAccessor)
            {
                this.messageRepository = messageRepository;
                this.httpContextAccessor = httpContextAccessor;
            }
            public async Task<PagedList<MessageDto>> Handle(GetAlMessageForUserQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    request.MessageParams.UserName = httpContextAccessor?.HttpContext?.User?.Claims?
                                                    .FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                    var message = await messageRepository.GetAlMessageForUser(request.MessageParams);
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
