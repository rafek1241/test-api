using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Test.API.Commands.Requests;
using Api.Test.API.Queries.Requests;
using Api.Test.Domain;
using Api.Test.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Page<Customer>))]
        public async Task<IActionResult> Get(
            CancellationToken token,
            int take = 10,
            int skip = 0
        )
        {
            var page = await _mediator.Send(new GetCustomers(take, skip), token);
            return Ok(page);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Customer))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken token)
        {
            var customer = await _mediator.Send(new GetCustomer(id), token);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Customer customer, CancellationToken token)
        {
            var id = await _mediator.Send(new RegisterCustomer(customer), token);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }
    }
}