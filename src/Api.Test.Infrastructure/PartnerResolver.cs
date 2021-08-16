using System;
using System.Collections.Generic;
using System.Linq;
using Api.Test.Partner;
using Api.Test.Partner.Exceptions;

namespace Api.Test.Infrastructure
{
    public class PartnerResolver<TService> : IPartnerResolver<TService> where TService : IPartnerService
    {
        private readonly IEnumerable<TService> _implementations;
        private readonly IPartnerReceiver _partnerReceiver;

        public PartnerResolver(IEnumerable<TService> implementations, IPartnerReceiver partnerReceiver)
        {
            _implementations = implementations;
            _partnerReceiver = partnerReceiver;
        }

        public TService Resolve()
        {
            var currentPartner = _partnerReceiver.Receive();
            var partnerServices = _implementations
                .Where(x => x.Partner == currentPartner)
                .ToArray();
            if (partnerServices.Length > 1)
            {
                throw new TooManyServicesForSamePartner<TService>(currentPartner, partnerServices);
            }
            
            if (partnerServices.Any() == false)
            {
                throw new NoServiceForPartner<TService>(currentPartner);
            }

            return partnerServices.Single();
        }
    }
}