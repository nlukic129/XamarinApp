using FluentValidation;
using Nebojsa_60_19.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebojsa_60_19.Validators
{
    internal class RegistrationPageViewValidator : AbstractValidator<RegisterPageViewModel>
    {
        public RegistrationPageViewValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MinimumLength(3).WithMessage("Minimum number of character is 3")
                .Matches(@"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})*$").WithMessage("First name is not in the correct format");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MinimumLength(3).WithMessage("Minimum number of character is 3")
                .Matches(@"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})*$").WithMessage("Last name is not in the correct format");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("Email address is not in the correct format");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(4).WithMessage("Minimum number of character is 4")
                .Matches("^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                .WithMessage("It is possible to use only a combination of letters and numbers. and _");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(4).WithMessage("Minimum number of character is 8")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("It is necessary to enter at least one lowercase and one uppercase letter, a number and a special character");
        }
    }
}
