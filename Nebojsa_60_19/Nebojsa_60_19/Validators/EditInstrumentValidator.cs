using FluentValidation;
using Nebojsa_60_19.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebojsa_60_19.Validators
{
    public class EditInstrumentValidator : AbstractValidator<EditInstrumentPageViewModel>
    {
        public EditInstrumentValidator()
        {
            RuleFor(x => x.Year).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Year is required")
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("Whitespaces only are not allowed");

            RuleFor(x => x.Country).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Country is required")
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("Whitespaces only are not allowed");

            RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Description is required")
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("Whitespaces only are not allowed");
        }
    }
}
