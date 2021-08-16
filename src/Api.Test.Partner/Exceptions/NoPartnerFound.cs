using System;

namespace Api.Test.Partner.Exceptions
{
    public class NoPartnerFound : ArgumentNullException
    {
        public NoPartnerFound(string param, string message) 
            : base(param, message)
        {
            
        }
    }
}