using AutoMapper;
using Boleyn.Database.Entities.Contents;
using Cms.Endpoints.Article.Get;
using FizzWare.NBuilder;
using Shouldly;
using Xunit;

namespace Api.Unit.Tests.EndpointTests.Article.Get
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
            var response = _mapper.Map<Response>(TestContent);

            response.ShouldNotBeNull();
            response.Description.ShouldBeEquivalentTo(TestContent.Description);
            response.Summary.ShouldBeEquivalentTo(TestContent.Summary);
            response.Title.ShouldBeEquivalentTo(TestContent.Title);
            response.ShouldSatisfyAllConditions();
           
        }

        private static Content TestContent => Builder<Content>
            .CreateNew()
            .Build();
        
    }
}