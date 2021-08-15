using Api.Test.Domain.Models;
using FluentValidation;

namespace Api.Test.Partner.Interfaces
{
    public interface ICustomerValidator : IPartnerService, IValidator<Customer>
    { }
}