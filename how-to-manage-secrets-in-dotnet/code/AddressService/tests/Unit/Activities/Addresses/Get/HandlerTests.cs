using AutoMapper;
using Domain;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using Services;
using Shouldly;
using Threenine.Activities.Addresses.Queries.Get;
using Xunit;

namespace Threenine.Activities.Addresses.Get;

public class HandlerTests
{
    private readonly Handler _classUnderTest;
    private readonly IMapper _mapper;
    private readonly Mock<IAddressDataProvider> _addressDataProviderMock;

    public HandlerTests()
    {
        var mapperConfiguration = new MapperConfiguration(configuration => configuration.AddProfile<Mapping>());
        mapperConfiguration.AssertConfigurationIsValid();

        _mapper = mapperConfiguration.CreateMapper();
        _addressDataProviderMock = new Mock<IAddressDataProvider>();
        _classUnderTest = new Handler(_addressDataProviderMock.Object, _mapper);
    }

    private static Source ValidAddressResult => Builder<Source>.CreateNew()
        .With(x => x.Item = Builder<Item>.CreateListOfSize(10).Build().ToArray()).Build();

    [Fact]
    public async Task Should_return_a_list_of_addresses_in_response()
    {
        _addressDataProviderMock.Setup(x => x.GetByPostCode(It.IsAny<string>(), default)).ReturnsAsync(ValidAddressResult);

        var result = await _classUnderTest.Handle(new Query { PostCode = "xx1 1xx"}, default);

        result.ShouldSatisfyAllConditions(
            () => result.ShouldNotBeNull(),
            () => result.Item.Addresses.ShouldBeOfType<Address[]>()
            );
        
    }
    
}