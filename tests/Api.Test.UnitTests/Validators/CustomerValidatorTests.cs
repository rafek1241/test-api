using System;
using System.Threading.Tasks;
using Api.Test.Domain.Models;
using Api.Test.Domain.Validators;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Api.Test.UnitTests.Validators
{
    public class CustomerValidatorTests
    {
        private readonly IFixture _fixture = FixtureFactory.Instance;
        private readonly CustomerValidator _validator = new CustomerValidatorStub();
        
        
        internal class CustomerValidatorStub : CustomerValidator{}
        
        [Fact]
        public async Task when_validated_customer_missing_FirstName__returns_invalid()
        {
            var instance = _fixture.Build<Customer>()
                .Without(x => x.FirstName)
                .Create();

            var result = await _validator.TestValidateAsync(instance);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }
        
        [Fact]
        public async Task when_validated_customer_missing_LastName__returns_invalid()
        {
            var instance = _fixture.Build<Customer>()
                .Without(x => x.LastName)
                .Create();

            var result = await _validator.TestValidateAsync(instance);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Fact]
        public async Task when_validated_customer_missing_personal_number_and_favorite_football_team__returns_valid()
        {
            var instance = _fixture.Build<Customer>()
                .Without(x => x.PersonalNumber)
                .Without(x => x.FavoriteFootballTeam)
                .Create();

            var result = await _validator.TestValidateAsync(instance);
            result.ShouldNotHaveValidationErrorFor(x => x.PersonalNumber);
            result.ShouldNotHaveValidationErrorFor(x => x.FavoriteFootballTeam);
            result.IsValid.Should().BeTrue();
        }
    }
}