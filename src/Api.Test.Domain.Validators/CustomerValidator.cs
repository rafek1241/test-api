﻿using Api.Test.Domain.Models;
using FluentValidation;

namespace Api.Test.Domain.Validators
{
    public abstract class CustomerValidator : AbstractValidator<Customer>
    {
        protected CustomerValidator()
        {
            SetRules();
        }

        private void SetRules()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .NotEmpty();
            RuleFor(x => x.Address)
                .SetValidator(new AddressValidator());
        }
    }
}