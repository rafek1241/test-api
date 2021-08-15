using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Test.API.Commands.Requests;
using Api.Test.Domain;
using MediatR;

namespace Api.Test.API.Commands.Handlers
{
    public class RegisterCustomerHandler : IRequestHandler<RegisterCustomer, Guid>
    {
        private readonly ICompanyReceiver _companyReceiver;

        public RegisterCustomerHandler(ICompanyReceiver companyReceiver)
        {
            _companyReceiver = companyReceiver;
        }
        
        public Task<Guid> Handle(RegisterCustomer request, CancellationToken cancellationToken)
        {
            var company = _companyReceiver;

            throw new NotImplementedException();
        }
    }
}