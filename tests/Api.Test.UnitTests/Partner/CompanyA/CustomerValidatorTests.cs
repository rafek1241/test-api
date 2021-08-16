using System;
using Xunit;

namespace Api.Test.UnitTests.Partner.CompanyA
{
    public class CustomerValidatorTests
    {
        [Theory]
        [InlineData("xxxxx-xxx")]
        [InlineData("1234-223")]
        [InlineData("123444-223")]
        [InlineData("12344-23")]
        [InlineData("12344-2322")]
        [InlineData("adsadasdas")]
        [InlineData("1")]
        public void when_personal_number_passed_in_invalid_state__returns_invalid(string input)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void when_personal_number_passed_in_correct_state__returns_valid()
        {
            var input = Guid.NewGuid()
                .GetHashCode()
                .ToString("00000-000");
            
        }
    }
}