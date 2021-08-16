using System;
using System.Threading.Tasks;
using Api.Test.CompanyA;
using Api.Test.Domain.Models;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Api.Test.UnitTests.Partner.CompanyA
{
    public class CustomerValidatorTests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly IFixture _fixture = FixtureFactory.Instance;
        private CustomerValidator _validator;

        public CustomerValidatorTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _validator = _fixture.Create<CustomerValidator>();
        }

        [Theory]
        [InlineData("xxxxx-xxx")]
        [InlineData("1234-223")]
        [InlineData("123444-223")]
        [InlineData("12344-23")]
        [InlineData("12344-2322")]
        [InlineData("adsadasdas")]
        [InlineData("")]
        [InlineData("1")]
        public async Task when_personal_number_passed_in_invalid_state__returns_invalid(string input)
        {
            var instance = _fixture.Build<Customer>()
                .With(x => x.PersonalNumber, input)
                .Create();

            var result = await _validator.TestValidateAsync(instance);
            result.ShouldHaveValidationErrorFor(x => x.PersonalNumber);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task when_personal_number_passed_in_correct_state__returns_valid()
        {
            var input = Math.Abs(Guid.NewGuid()
                .GetHashCode() % 99999999)
                .ToString("00000-000");
            
            var instance = _fixture.Build<Customer>()
                .With(x => x.PersonalNumber, input)
                .Create();

            _outputHelper.WriteLine("Validated object: \n{0}", JsonConvert.SerializeObject(instance, Formatting.Indented));
            
            var result = await _validator.TestValidateAsync(instance);
            result.ShouldNotHaveValidationErrorFor(x=>x.PersonalNumber);
            result.IsValid.Should().BeTrue();
        }
    }
}