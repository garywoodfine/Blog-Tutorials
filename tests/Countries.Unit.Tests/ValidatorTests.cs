using Boleyn.Countries.Activities.Country.Get;
using FluentValidation.TestHelper;
using Xunit;

namespace Countries.Unit.Tests
{
    public class ValidatorTests
    {
        private readonly Validator _validator;

        public ValidatorTests()
        {
            _validator = new Validator();
        }

        [Theory]
        [InlineData("")]
        [InlineData("somelongcode")]
        [InlineData("a")]
        [InlineData("123")]
        public void Should_have_validation_error_for_empty_iso_code(string input)
        {
            var query = new Query{ IsoCode = input };
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.IsoCode);
        }
    }
}