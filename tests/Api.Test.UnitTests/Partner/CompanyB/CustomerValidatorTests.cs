using System;
using System.Threading.Tasks;
using Api.Test.CompanyB;
using Api.Test.Domain.Models;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Api.Test.UnitTests.Partner.CompanyB
{
    public class CustomerValidatorTests
    {
        private readonly IFixture _fixture = FixtureFactory.Instance;
        private CustomerValidator _validator;

        public CustomerValidatorTests()
        {
            _validator = _fixture.Create<CustomerValidator>();
        }

        
        [Fact]
        public async Task when_favorite_football_team_missing__returns_invalid()
        {
            var instance = _fixture.Build<Customer>()
                .Without(x => x.FavoriteFootballTeam)
                .Create();

            var result = await _validator.TestValidateAsync(instance);
            
            instance.FavoriteFootballTeam
                .Should()
                .BeNullOrEmpty();
            
            result.ShouldHaveValidationErrorFor(x => x.FavoriteFootballTeam);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task when_favorite_football_team_filled__returns_valid()
        {
            var instance = _fixture.Create<Customer>();
            
            var result = await _validator.TestValidateAsync(instance);
            
            instance.FavoriteFootballTeam.Should().NotBeEmpty();
            result.ShouldNotHaveValidationErrorFor(x => x.FavoriteFootballTeam);
            result.IsValid.Should().BeTrue();
        }
    }
}