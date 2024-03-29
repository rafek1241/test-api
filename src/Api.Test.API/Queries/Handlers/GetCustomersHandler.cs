using System.Threading;
using System.Threading.Tasks;
using Api.Test.API.Queries.Requests;
using Api.Test.Domain;
using Api.Test.Domain.Models;
using MediatR;

namespace Api.Test.API.Queries.Handlers
{
    public class GetCustomersHandler : IRequestHandler<GetCustomers, Page<Customer>>
    {
        private readonly IRepository<Customer> _repository;

        public GetCustomersHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Page<Customer>> Handle(
            GetCustomers request,
            CancellationToken token
        ) =>
            await _repository.Get(request.Skip, request.Take,token);
    }
}