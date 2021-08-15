using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Test.API.Commands.Requests;
using Api.Test.Domain;
using Api.Test.Domain.Models;
using Api.Test.Partner;
using Api.Test.Partner.Interfaces;
using FluentValidation;
using MediatR;

namespace Api.Test.API.Commands.Handlers
{
    public class RegisterCustomerHandler : IRequestHandler<RegisterCustomer, Guid>
    {
        private readonly IPartnerResolver<ICustomerValidator> _validationResolver;
        private readonly IRepository<Customer> _repository;

        public RegisterCustomerHandler(
            IPartnerResolver<ICustomerValidator> validationResolver,
            IRepository<Customer> repository
        )
        {
            _validationResolver = validationResolver;
            _repository = repository;
        }

        public async Task<Guid> Handle(RegisterCustomer request, CancellationToken cancellationToken)
        {
            var validator = _validationResolver.Resolve();
            await validator.ValidateAndThrowAsync(request.Customer, cancellationToken);
            
            return await _repository.Post(request.Customer);
        }
    }
}