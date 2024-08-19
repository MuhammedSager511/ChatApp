using Application.Features.Message.Command.AddMessage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Message.Validator
{
    public class MessageValidator:AbstractValidator<AddMessageDto>
    {
        public MessageValidator()
        {
            RuleFor(x=>x.Content).NotNull().WithMessage("{PropertyName} is not null")
                .MinimumLength(3).WithMessage("{PropertyName} Min Length {PropertyValue}");
        }
    }
}
