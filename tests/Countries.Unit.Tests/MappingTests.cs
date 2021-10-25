using AutoMapper;
using Boleyn.Countries.Activities.Country.Get;
using FizzWare.NBuilder;
using Shouldly;
using WorldBank.Models;
using Xunit;

namespace Countries.Unit.Tests
{
    public class MappingTests
    {
        private IMapper _mapper;
        
        public MappingTests()
        {
            var mapperConfiguration = new MapperConfiguration(configuration => configuration.AddProfile<Mapping>());
            mapperConfiguration.AssertConfigurationIsValid();
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public void Should_Entity_to_Data_Transmission_Object()
        {
            var response = _mapper.Map<Response>(TestCountry);

            response.ShouldNotBeNull();
            response.Name.ShouldBeEquivalentTo(TestCountry.Name);
            response.Region.ShouldBeEquivalentTo(TestCountry.Region.Value);
            response.Latitude.ShouldBeEquivalentTo(TestCountry.Latitude);
            response.Longitude.ShouldBeEquivalentTo(TestCountry.Longitude);
            response.Capital.ShouldBeEquivalentTo(TestCountry.CapitalCity);
            response.ShouldSatisfyAllConditions();
           
        }

        private static Country TestCountry => Builder<Country>
            .CreateNew()
            .With(x => x.Region = Builder<AdminRegion>
                .CreateNew()
                .Build()
            ).Build();
    }
}