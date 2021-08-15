using System;
using Api.Test.Domain.Models;
using MediatR;

namespace Api.Test.API.Commands.Requests
{
    public class RegisterCustomer : IRequest<Guid>
    {
        public Customer Customer { get; private set; }

        public RegisterCustomer(Customer customer)
        {
            Customer = customer;
        }
    }
}