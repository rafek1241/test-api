using System.Threading;
using System.Threading.Tasks;
using Api.Test.API.Queries.Requests;
using Api.Test.Domain;
using Api.Test.Domain.Models;
using MediatR;

namespace Api.Test.API.Queries.Handlers
{
    public class GetCustomerHandler : IRequestHandler<GetCustomer, Customer>
    {
        private readonly IRepository<Customer> _repository;

        public GetCustomerHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public Task<Customer> Handle(
            GetCustomer request,
            CancellationToken cancellationToken
        ) =>
            _repository.Get(request.Id, cancellationToken);
    }
}