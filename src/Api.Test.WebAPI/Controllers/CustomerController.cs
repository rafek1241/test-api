using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Api.Test.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return new[]
            {
                new { Model = "elo1" },
                new { Model = "elo2" }
            };
        }
    }
}