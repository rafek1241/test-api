using System;

namespace Api.Test.Partner.Exceptions
{
    public class NoServiceForPartner<T> : Exception where T : IPartnerService
    {
        public NoServiceForPartner(PartnerEnum partner)
            : base($"There is no service of type '{typeof(T).Name}' for partner '{partner}' registered.")
        {
        }
    }
}