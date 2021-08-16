using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Api.Test.API.Filters
{
    public class GeneralExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public GeneralExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            _logger.Error(context.Exception, context.Exception.Message);
            context.Result = new ObjectResult("Please contact with administrator.")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }
}