using System.Threading.Tasks;
using Api.Endpoints.Article;
using Api.Endpoints.Article.Request;
using FizzWare.NBuilder;
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

            var result = await _classUnderTest.HandleAsync(request, default);
            result.ShouldNotBeNull();
        }
    }
}