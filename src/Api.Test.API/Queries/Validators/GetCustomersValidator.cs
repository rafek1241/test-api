using Api.Test.API.Queries.Requests;
using FluentValidation;

namespace Api.Test.API.Queries.Validators
{
    public class GetCustomersValidator : AbstractValidator<GetCustomers>
    {
        public GetCustomersValidator()
        {
            RuleFor(x => x.Take)
                .InclusiveBetween(1,100);

            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0);
        }
    }
}