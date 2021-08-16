using System;
using Api.Test.API.Exceptions;
using Api.Test.Domain;
using Api.Test.Partner;
using Api.Test.Partner.Exceptions;
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

        public PartnerEnum Receive()
        {
            var request = _contextAccessor.HttpContext.Request;
            var cookieKey = Constraints.PartnerCookieKey;
            var containsCookie = request.Cookies
                .TryGetValue(cookieKey, out var cookie);

            if (containsCookie == false)
            {
                throw new NoCookiePassed(cookieKey);
            }

            var parsed = Enum.TryParse(typeof(PartnerEnum), cookie, true, out var result);
            var nullableResult = (PartnerEnum?)result;
            var resultEnum = nullableResult.GetValueOrDefault();

            if (resultEnum == default || parsed == false)
            {
                throw new NoPartnerFound(
                    nameof(cookie),
                    $"Cookie '{cookieKey}={cookie}' in request has wrong value. We don't support such company for that operation"
                );
            }

            return resultEnum;
        }
    }
}