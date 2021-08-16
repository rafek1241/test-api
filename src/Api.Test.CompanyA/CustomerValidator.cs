using System.Text.RegularExpressions;
using Api.Test.Partner;
using Api.Test.Partner.Interfaces;
using FluentValidation;

namespace Api.Test.CompanyA
{
    public class CustomerValidator : Domain.Validators.CustomerValidator, ICustomerValidator
    {
        private readonly Regex _regex = new Regex("^(\\d){5}-(\\d){3}$");

        public CustomerValidator()
        {
            RuleFor(x => x.PersonalNumber)
                .NotEmpty()
                .Must(input => _regex.IsMatch(input))
                .WithMessage(customer => $"Should be of format `XXXXX-XXX` where X is a number");
        }

        public PartnerEnum Partner { get; } = PartnerEnum.CompanyA;
    }
}