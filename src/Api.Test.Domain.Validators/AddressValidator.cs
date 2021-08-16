using Api.Test.Domain.Models;
using FluentValidation;

namespace Api.Test.Domain.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty();
            RuleFor(x => x.Street)
                .NotEmpty();
            RuleFor(x => x.ZipCode)
                .NotEmpty();
        }
    }
}