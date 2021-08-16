using System;
using System.Collections.Generic;
using System.Linq;
using Api.Test.Infrastructure;
using Api.Test.Partner;
using Api.Test.Partner.Exceptions;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Api.Test.UnitTests.Infrastructure
{
    public class PartnerResolverTests
    {
        private readonly IFixture _fixture = FixtureFactory.Instance;
        private readonly IPartnerReceiver _partnerReceiver;
        private PartnerResolver<IPartnerService> Sut => CreateSystemUnderTest();

        public PartnerResolverTests()
        {
            _partnerReceiver = _fixture.Freeze<IPartnerReceiver>();
        }

        [Fact]
        public void when_many_implementations_registered_and_one_of_them_contains_received_partner__returns_specific_partner_service()
        {
            var partner = PartnerEnum.CompanyA;
            var services = CreatePartnerServices(PartnerEnum.CompanyA, PartnerEnum.CompanyB)
                .ToArray();
            var companyAService = services.Single(x => x.Partner == partner);
            _fixture.Inject<IEnumerable<IPartnerService>>(services);
            _partnerReceiver
                .Receive()
                .Returns(partner);

            var implementation = Sut.Resolve();

            implementation.Should()
                .Be(companyAService);
        }

        [Fact]
        public void when_many_implementations_and_several_of_them_contains_same_partner__throws_TooManyServicesForSamePartner()
        {
            var partner = PartnerEnum.CompanyA;
            var services = CreatePartnerServices(PartnerEnum.CompanyA, PartnerEnum.CompanyA, PartnerEnum.CompanyB)
                .ToArray();
            _fixture.Inject<IEnumerable<IPartnerService>>(services);
            _partnerReceiver
                .Receive()
                .Returns(partner);

            Action handler = () => Sut.Resolve();

            handler
                .Should()
                .Throw<TooManyServicesForSamePartner<IPartnerService>>();
        }

        [Fact]
        public void when_there_is_no_implementation_for_received_partner__throws_NoServiceForPartner()
        {
            var partner = PartnerEnum.CompanyA;
            var services = CreatePartnerServices(PartnerEnum.CompanyB)
                .ToArray();
            _fixture.Inject<IEnumerable<IPartnerService>>(services);
            _partnerReceiver
                .Receive()
                .Returns(partner);

            Action handler = () => Sut.Resolve();

            handler
                .Should()
                .Throw<NoServiceForPartner<IPartnerService>>();
        }

        private IEnumerable<IPartnerService> CreatePartnerServices(params PartnerEnum[] names)
        {
            foreach (var name in names)
            {
                var mock = Substitute.For<IPartnerService>();
                mock.Partner.Returns(name);
                yield return mock;
            }
        }

        private PartnerResolver<IPartnerService> CreateSystemUnderTest()
            => _fixture.Create<PartnerResolver<IPartnerService>>();
    }
}