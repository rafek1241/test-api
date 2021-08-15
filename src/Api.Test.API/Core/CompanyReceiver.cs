using System;
using Api.Test.Domain;
using Microsoft.AspNetCore.Http;

namespace Api.Test.API.Core
{
    public class CompanyReceiver : ICompanyReceiver
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CompanyReceiver(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        
        public Company? Receive()
        {
            var request = _contextAccessor.HttpContext.Request;
            var cookieKey = nameof(Company).ToLower();
            var containsCookie = request.Cookies
                .TryGetValue(cookieKey, out var cookie);

            if (containsCookie == false)
            {
                return null;
            }
            
            var parsed = Enum.TryParse(typeof(Company), cookie, true, out var result);

            if (parsed == false)
            {
                throw new NotSupportedException($"Cookie '{cookieKey}={cookie}' in request has wrong value. We don't support such company for that operation.");
            }

            if (result == null)
            {
                
            }

            return result as Company?;
        }
    }
}