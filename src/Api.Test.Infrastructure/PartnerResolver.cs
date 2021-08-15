using System;
using System.Collections.Generic;
using System.Linq;
using Api.Test.Partner;

namespace Api.Test.Infrastructure
{
    public class PartnerResolver<TService> : IPartnerResolver<TService> where TService : IPartnerService
    {
        private readonly IEnumerable<TService> _implementations;
        private readonly ICompanyReceiver _companyReceiver;

        public PartnerResolver(IEnumerable<TService> implementations, ICompanyReceiver companyReceiver)
        {
            _implementations = implementations;
            _companyReceiver = companyReceiver;
        }
        
        public TService Resolve()
        {
            var currentPartner = _companyReceiver.Receive();
            if (currentPartner == null)
            {
                throw new ArgumentNullException(nameof(currentPartner), "Cannot resolve partner.");
            }

            var service = _implementations.SingleOrDefault(x => x.Partner == currentPartner);
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service), $"Service cannot be resolved because it does not exists for particular partner. \nPartner: '{currentPartner}', Tried to resolve: {typeof(TService).Name}");
            }
            
            return service;
        }
    }
}