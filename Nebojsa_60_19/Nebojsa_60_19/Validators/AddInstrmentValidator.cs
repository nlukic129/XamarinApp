using FluentValidation;
using Nebojsa_60_19.Database;
using Nebojsa_60_19.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebojsa_60_19.Validators
{
    public class AddInstrmentValidator : AbstractValidator<AddInstrumentPageViewModel>
    {
        private Db db;
        public AddInstrmentValidator(Db db)
        {
            this.db = db;
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Name is required")
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("Whitespaces only are not allowed")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
                .Must(NameIsNotInUse).WithMessage("Name address is already in use");

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

        private bool NameIsNotInUse(string name)
        {
            var nameInUse = db.Connection.Find<Instruments>(x => x.Name == name);
            return nameInUse == null;
        }
    }
}
