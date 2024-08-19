using Application.Features.Message.Validator;
using Application.Presistence.Contracts;
using Application.Responses;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
            private readonly IMapper mapper;

            public Handler(IMessageRepository messageRepository,IMapper mapper)
            {
                this.messageRepository = messageRepository;
                this.mapper = mapper;
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


                    await messageRepository.AddAsync(mapper.Map<Domain.Entities.Message>(request.AddMessageDto));
                    response.IsSuccess = true;
                    response.Message = "Success Adding New Message";
                 

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
