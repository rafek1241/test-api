using System;
using Api.Test.Domain.Models;
using MediatR;

namespace Api.Test.API.Queries.Requests
{
    public class GetCustomer : IRequest<Customer>
    {
        public Guid Id { get; private set; }

        public GetCustomer(Guid id)
        {
            Id = id;
        }
    }
}