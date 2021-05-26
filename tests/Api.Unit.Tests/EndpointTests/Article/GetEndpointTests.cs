using System;
using System.Threading.Tasks;
using Cms.Endpoints.Article.Get;
using FizzWare.NBuilder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Api.Unit.Tests.EndpointTests.Article
{
    public class GetEndpointTests
    {
        private readonly Get _classUnderTest;
        private Mock<IMediator> _mediator;
        public GetEndpointTests()
        {
            _mediator = new Mock<IMediator>();
            _classUnderTest = new Get(_mediator.Object);
        }
        [Fact]
        public async Task Should_return_ok_object_result_with_article_response()
        {
            // Arrange
            var request = Builder<GetArticleQuery>.CreateNew().Build();
            var response = Builder<GetArticleResponse>.CreateNew().Build();
            _mediator.Setup(x => x.Send(It.IsAny<GetArticleQuery>(), default)).ReturnsAsync(response);
            // Act 
            var result = await _classUnderTest.HandleAsync(request, default);
            
            // Assert
            result.ShouldNotBeNull();
            result.Result.ShouldBeOfType<OkObjectResult>();
            result.ShouldSatisfyAllConditions();

            var okResult = result.Result as OkObjectResult;
            okResult.Value.ShouldBeOfType<GetArticleResponse>();
            okResult.ShouldSatisfyAllConditions();
        }
        [Fact]
        public async Task Should_return_404_response()
        {
            // Arrange
            var request = Builder<GetArticleQuery>.CreateNew().Build();
           _mediator.Setup(x => x.Send(It.IsAny<GetArticleQuery>(), default)).ReturnsAsync((Func<GetArticleResponse>) null);
            // Act 
            var result = await _classUnderTest.HandleAsync(request, default);
            
            // Assert
            result.ShouldNotBeNull();
            result.Result.ShouldBeOfType<NotFoundResult>();
            result.ShouldSatisfyAllConditions();
        }
    }
}