using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Test.Partner.Exceptions
{
    public class TooManyServicesForSamePartner<T> : Exception where T : IPartnerService
    {
        public TooManyServicesForSamePartner(PartnerEnum partner, IEnumerable<T> services) 
            : base($"Too many services of type '{typeof(T).Name}' assigned to '{partner}'." +
                $"\n Services: " +
                $"\n- {string.Join("\n- ", services.Select(x => x.GetType().AssemblyQualifiedName))}"
            )
        {
        }
    }
}