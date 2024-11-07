using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Command.Register
{
    public class RegisterValidator:AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName).NotNull()
                .NotEmpty().WithMessage("User Name Not Empty !")
                .MinimumLength(3).WithMessage("{PropertyName} Limited 3 character");

            RuleFor(x => x.Email).NotNull()
               .NotEmpty().WithMessage("{PropertyName} is required !")
               .EmailAddress().WithMessage("email address not valid");

            RuleFor(x => x.Password).NotNull()
              .NotEmpty().WithMessage("{PropertyName} is required !")
              .MinimumLength(8).WithMessage("{PropertyName} Length must be at least 8 ")
              .MaximumLength(15).WithMessage("{PropertyName} Length must be not exceed 15")
              .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
              .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
              .Matches(@"[0-9]+").WithMessage("Password must contain at least one digit.");

            RuleFor(x => x.KnownAs).NotNull()
            .NotEmpty().WithMessage(" KnownAs is required !")
            .MinimumLength(3).WithMessage("{PropertyName} Length must be at least 3 character ")
            .MaximumLength(5).WithMessage("{PropertyName} Length must be not exceed 5  character");




        }






        //private void beAl15Yeareolt(int time)
        //{
        //    int age = 2024- time;
        //    if(time>2015)
        //       return age == 10;

        //}
    }
}
