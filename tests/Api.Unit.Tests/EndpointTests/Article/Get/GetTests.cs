using System;
using System.Threading.Tasks;
using Cms.Endpoints.Article.Get;
using FizzWare.NBuilder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Api.Unit.Tests.EndpointTests.Article.Get
{
    public class GetTests
    {
        private readonly Cms.Endpoints.Article.Get.Get _classUnderTest;
        private Mock<IMediator> _mediator;
        public GetTests()
        {
            _mediator = new Mock<IMediator>();
            _classUnderTest = new Cms.Endpoints.Article.Get.Get(_mediator.Object);
        }
        [Fact]
        public async Task Should_return_ok_object_result_with_article_response()
        {
            // Arrange
            var request = Builder<Query>.CreateNew().Build();
            var response = Builder<Response>.CreateNew().Build();
            _mediator.Setup(x => x.Send(It.IsAny<Query>(), default)).ReturnsAsync(response);
            // Act 
            var result = await _classUnderTest.HandleAsync(request, default);
            
            // Assert
            result.ShouldNotBeNull();
            result.Result.ShouldBeOfType<OkObjectResult>();
            result.ShouldSatisfyAllConditions();

            var okResult = result.Result as OkObjectResult;
            okResult.Value.ShouldBeOfType<Response>();
            okResult.ShouldSatisfyAllConditions();
        }
        [Fact]
        public async Task Should_return_404_response()
        {
            // Arrange
            var request = Builder<Query>.CreateNew().Build();
           _mediator.Setup(x => x.Send(It.IsAny<Query>(), default)).ReturnsAsync((Func<Response>) null);
            // Act 
            var result = await _classUnderTest.HandleAsync(request, default);
            
            // Assert
            result.ShouldNotBeNull();
            result.Result.ShouldBeOfType<NotFoundResult>();
            result.ShouldSatisfyAllConditions();
        }
    }
}