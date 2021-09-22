using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace WorldBank.Models.Tests
{
    public class CountrySerializationTests
    {
        [Fact]
        public void Should_Serialise_World_Bank_Response()
        {
            var obj = JsonConvert.DeserializeObject<JArray>(ValidJsonResponse);
           
            obj.ShouldBeAssignableTo<JArray>();
            obj.Count.ShouldBeEquivalentTo(2);

            var country = JsonConvert.DeserializeObject<Country>(obj[1][0].ToString());
            country.ShouldBeAssignableTo<Country>();
            country.Name.ShouldBeEquivalentTo("Brazil");
            country.Region.Value.ShouldBeEquivalentTo("Latin America & Caribbean");

        }
        
        private static string ValidJsonResponse => "[{\"page\":1,\"pages\":1,\"per_page\":\"50\",\"total\":1},[{\"id\":\"BRA\",\"iso2Code\":\"BR\",\"name\":\"Brazil\",\"region\":{\"id\":\"LCN\",\"iso2code\":\"ZJ\",\"value\":\"Latin America & Caribbean\"},\"adminregion\":{\"id\":\"LAC\",\"iso2code\":\"XJ\",\"value\":\"Latin America & Caribbean (excluding high income)\"},\"incomeLevel\":{\"id\":\"UMC\",\"iso2code\":\"XT\",\"value\":\"Upper middle income\"},\"lendingType\":{\"id\":\"IBD\",\"iso2code\":\"XF\",\"value\":\"IBRD\"},\"capitalCity\":\"Brasilia\",\"longitude\":\"-47.9292\",\"latitude\":\"-15.7801\"}]]";
    }
}