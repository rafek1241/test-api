using Api.Test.Domain;
using Api.Test.Partner.Interfaces;
using FluentValidation;

namespace Api.Test.CompanyB
{
    public class CustomerValidator : Domain.Validators.CustomerValidator, ICustomerValidator
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FavoriteFootballTeam)
                .NotEmpty();
        }

        public Company Partner { get; } = Company.CompanyB;
    }
}