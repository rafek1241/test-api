using System;
using Api.Test.Domain;
using Api.Test.Partner;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Api.Test.API.Core
{
    public class PartnerReceiver : IPartnerReceiver
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public PartnerReceiver(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Partner.Partner? Receive()
        {
            var request = _contextAccessor.HttpContext.Request;
            var cookieKey = nameof(Partner.Partner)
                .ToLower();
            var containsCookie = request.Cookies
                .TryGetValue(cookieKey, out var cookie);

            if (containsCookie == false)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure($"Cookie `{cookieKey}`", "cookie needs to be filled.")
                });
            }

            var parsed = Enum.TryParse(typeof(Partner.Partner), cookie, true, out var result);

            if (parsed == false)
            {
                throw new NotSupportedException($"Cookie '{cookieKey}={cookie}' in request has wrong value. We don't support such company for that operation.");
            }

            return result as Partner.Partner?;
        }
    }
}