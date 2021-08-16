using System.Threading.Tasks;
using Api.Test.Domain.Models;
using Api.Test.Domain.Validators;
using AutoFixture;
using FluentValidation.TestHelper;
using Xunit;

namespace Api.Test.UnitTests.Validators
{
    public class AddressValidatorTests
    {
        private readonly IFixture _fixture = FixtureFactory.Instance;
        private readonly AddressValidator _validator;

        public AddressValidatorTests()
        {
            _validator = _fixture.Create<AddressValidator>();
        }

        [Fact]
        public async Task when_validated_customer_missing_AddressNumber__returns_invalid()
        {
            var instance = _fixture.Build<Address>()
                .Without(x => x.Number)
                .Create();

            var result = await _validator.TestValidateAsync(instance);
            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        [Fact]
        public async Task when_validated_customer_missing_AddressStreet__returns_invalid()
        {
            var instance = _fixture.Build<Address>()
                .Without(x => x.Street)
                .Create();

            var result = await _validator.TestValidateAsync(instance);
            result.ShouldHaveValidationErrorFor(x => x.Street);
        }

        [Fact]
        public async Task when_validated_customer_missing_AddressZipCode__returns_invalid()
        {
            var instance = _fixture.Build<Address>()
                .Without(x => x.ZipCode)
                .Create();

            var result = await _validator.TestValidateAsync(instance);
            result.ShouldHaveValidationErrorFor(x => x.ZipCode);
        }
    }
}