using Api.Test.Domain;
using Api.Test.Domain.Models;
using MediatR;

namespace Api.Test.API.Queries.Requests
{
    public class GetCustomers : IRequest<Page<Customer>>
    {
        public int Take { get; private set; }
        public int Skip { get; private set; }

        public GetCustomers(int take, int skip)
        {
            Take = take;
            Skip = skip;
        }
    }
}