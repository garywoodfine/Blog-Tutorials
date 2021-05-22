using System.Threading;
using System.Threading.Tasks;
using Cms.Endpoints.Article;
using Cms.Endpoints.Article.Get;
using FizzWare.NBuilder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Api.Unit.Tests.EndpointTests.Article
{
    public class GetHandlerTests
    {
        private readonly Get _classUnderTest;
        private Mock<IMediator> _mediator;
        public GetHandlerTests()
        {
            _mediator = new Mock<IMediator>();
            _classUnderTest = new Get(_mediator.Object);
        }
        [Fact]
        public async Task Should_return_ok_object_result_with_article_response()
        {
            var request = Builder<GetArticleQuery>.CreateNew().Build();
            var response = Builder<GetArticleResponse>.CreateNew().Build();

            _mediator.Setup(x => x.Send(It.IsAny<GetArticleQuery>(), default)).ReturnsAsync(response);
            var result = await _classUnderTest.HandleAsync(request, default);
            result.ShouldNotBeNull();
            result.Result.ShouldBeOfType<OkObjectResult>();

            var okResult = result.Result as OkObjectResult;
            okResult.Value.ShouldBeOfType<GetArticleResponse>();
            okResult.ShouldSatisfyAllConditions();

        }
    }
}