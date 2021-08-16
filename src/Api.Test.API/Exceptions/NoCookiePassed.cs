using System;

namespace Api.Test.API.Exceptions
{
    public class NoCookiePassed : Exception
    {
        public NoCookiePassed(string cookieKey)
            : base($"No cookie '{cookieKey}' passed to current operation.")
        { }
    }
}