using Application.Presistence.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Query.GetAllMessages
{
    public class GetAlMessageQuery:IRequest<List<MessageReturnDto>>
    {

        class Handler : IRequestHandler<GetAlMessageQuery, List<MessageReturnDto>>
        {
            private readonly IMessageRepository messageRepository;
            private readonly IMapper mapper;

            public Handler(IMessageRepository messageRepository,IMapper mapper)
            {
                this.messageRepository = messageRepository;
                this.mapper = mapper;
            }

          

            public async Task<List<MessageReturnDto>> Handle(GetAlMessageQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var allMessages =await messageRepository.GetAllAsync();
                    var mappingMessage=mapper.Map<List<MessageReturnDto>>(allMessages);
                    return mappingMessage;

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
