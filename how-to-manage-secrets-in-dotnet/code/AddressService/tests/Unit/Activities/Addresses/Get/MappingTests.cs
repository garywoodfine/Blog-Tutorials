using AutoMapper;
using Domain;
using FizzWare.NBuilder;
using Shouldly;
using Threenine.Activities.Addresses.Queries.Get;
using Xunit;

namespace Threenine.Activities.Addresses.Get;

public class MappingTests
{
    private readonly IMapper _mapper;

    public MappingTests()
    {
        var mapperConfiguration = new MapperConfiguration(configuration => configuration.AddProfile<Mapping>());
        mapperConfiguration.AssertConfigurationIsValid();
        _mapper = mapperConfiguration.CreateMapper();
    }
    
    private static Source  TestSourceData => Builder<Source>.CreateNew()
        .With(x => x.Item = Builder<Item>.CreateListOfSize(20).Build().ToArray())
        .Build();
    
    [Fact]
    public void Should_Map_SourceData_to_Response()
    {
        var response = _mapper.Map<Address[]>(TestSourceData.Item);

        response.ShouldSatisfyAllConditions(
            () => response.ShouldBeOfType<Address[]>(),
            () => response.Length.ShouldBeEquivalentTo(20)
        );
    }
    
    [Fact]
    public void Should_Map_Item_to_AddressItem()
    {
        var item = Builder<Item>.CreateNew().Build();

        var response = _mapper.Map<Address>(item);

        response.ShouldSatisfyAllConditions(
            () => response.ShouldBeOfType<Address>(),
            () => response.City.ShouldBeEquivalentTo(item.Town),
            () => response.County.ShouldBeEquivalentTo(item.OptionalCounty),
            () => response.Organisation.ShouldBeEquivalentTo(item.Organisation),
            () => response.Property.ShouldBeEquivalentTo(item.Property),
            () => response.Postcode.ShouldBeEquivalentTo(item.Postcode)
        );
    }
}