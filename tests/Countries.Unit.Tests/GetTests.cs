using System;
using System.Threading.Tasks;
using Boleyn.Countries.Activities.Country.Get;
using Boleyn.Countries.Content.Exceptions;
using FizzWare.NBuilder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Threenine.ApiResponse;
using Xunit;

namespace Countries.Unit.Tests
{
    public class GetTests
    {
        private readonly Get _classUnderTest;
        private Mock<IMediator> _mediator;

        public GetTests()
        {
            _mediator = new Mock<IMediator>();
            _classUnderTest = new Get(_mediator.Object);
        }

        [Fact]
        public async Task Should_return_ok_object_result_with_country_response()
        {
            // Arrange
            var request = Builder<Query>.CreateNew().Build();
            var response =  new SingleResponse<Response>(Builder<Response>.CreateNew().Build());
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
            _mediator.Setup(x => x.Send(It.IsAny<Query>(), default)).ReturnsAsync((Func<SingleResponse<Response>>)null);
            // Act 
            var result = await _classUnderTest.HandleAsync(request, default);

            // Assert
            result.ShouldNotBeNull();
            result.Result.ShouldBeOfType<NotFoundResult>();
            result.ShouldSatisfyAllConditions();
        }

    }
}