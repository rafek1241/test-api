using System.Collections.Generic;
using Api.Test.Domain.Models;
using MediatR;

namespace Api.Test.API.Queries.Requests
{
    public class GetCustomers : IRequest<IEnumerable<Customer>>
    { }
}