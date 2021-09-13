using Cms.Endpoints.Article.Get;
using FluentValidation.TestHelper;
using Xunit;

namespace Api.Unit.Tests.EndpointTests.Article.Get
{
    public class ValidatorTests
    {
        private readonly Validator _validator;
        public ValidatorTests()
        {
            _validator = new Validator();
        }

        [Fact(DisplayName = "Should have a validation error for empty id")]
       
        public void Should_have_validation_error_for_empty_id()
        {
            var query = new Query();
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}