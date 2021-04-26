using System.Threading.Tasks;
using Cms.Endpoints.Article;
using Cms.Endpoints.Article.Request;
using Cms.Endpoints.Article.Response;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace Api.Unit.Tests.EndpointTests.Article
{
    public class GetHandlerTests
    {
        private readonly Get _classUnderTest;
        public GetHandlerTests()
        {
            _classUnderTest = new Get();
        }
        [Fact]
        public async Task Should_return_ok_object_result_with_article_response()
        {
            var request = Builder<ArticleRequest>.CreateNew().Build();

            var response = await _classUnderTest.HandleAsync(request, default);
            response.ShouldNotBeNull();
            response.Result.ShouldBeOfType<OkObjectResult>();

            var result = response.Result as OkObjectResult;
            result.Value.ShouldBeOfType<ArticleResponse>();
            response.ShouldSatisfyAllConditions();

        }
    }
}