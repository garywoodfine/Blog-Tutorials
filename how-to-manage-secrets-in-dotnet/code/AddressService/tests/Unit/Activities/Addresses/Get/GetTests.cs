using System.ComponentModel;
using FizzWare.NBuilder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Threenine.Activities.Addresses.Queries.Get;
using Threenine.ApiResponse;
using Xunit;

namespace Threenine.Activities.Addresses.Get;

public class GetTests
{
    private readonly Threenine.Activities.Addresses.Queries.Get.Get _classUnderTest;
    private readonly Mock<IMediator> _mediatorMock;

    public GetTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _classUnderTest = new Queries.Get.Get(_mediatorMock.Object);
    }

    private static Response ValidResponseObject => Builder<Response>.CreateNew()
        .With(x => x.Addresses = Builder<Address>.CreateListOfSize(20).Build().ToArray())
        .Build();
    
    [Fact(DisplayName = "Should make a verified call to mediatr")]
    [Description("The GET endpoint makes a call to mediatr handler to action the request")]
    public async Task Should_make_verified_call_to_mediatr()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<Query>(), default))
            .ReturnsAsync(new SingleResponse<Response>(ValidResponseObject ) );

        await _classUnderTest.HandleAsync(new Query { PostCode = "TestCode"});

        _mediatorMock.Verify(x => x.Send(It.IsAny<Query>(), default), Times.Once);
    }

    [Fact(DisplayName = "Should get a valid response object returned")]
    [Description("If the handler executes a valid request we should get a response")]
    public async Task Should_get_a_valid_response_object_returned()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<Query>(), default))
            .ReturnsAsync(new SingleResponse<Response>(ValidResponseObject ) );

        var result = await _classUnderTest.HandleAsync(new Query { PostCode = "TestCode" });

        result.ShouldSatisfyAllConditions(
            () => result.Result.ShouldNotBeNull(),
            () =>  result.Result.ShouldBeOfType<OkObjectResult>()
            );

        var okObjectResult = result.Result as OkObjectResult;
        
        okObjectResult.ShouldSatisfyAllConditions(
            ()=> okObjectResult.Value.ShouldBeOfType<Response>()
            );

    }
    
}