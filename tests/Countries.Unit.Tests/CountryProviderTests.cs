using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Boleyn.Countries.Content.Exceptions;
using Boleyn.Countries.Content.Providers;
using Moq;
using Moq.Protected;
using Shouldly;
using WorldBank.Models;
using Xunit;

namespace Countries.Unit.Tests
{
     public class CountryProviderTests
    {
        private readonly CountryProvider _classUnderTest;

        private Mock<HttpMessageHandler> _handlerMock;
        private HttpClient _client;

        public CountryProviderTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>();
            _client = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("http://somefakeUrl")
            };

            _classUnderTest = new CountryProvider(_client);
        }

        [Fact]
        public async Task Should_return_a_country()
        {
            _handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .Returns(Task.FromResult(ValidCountryResponse));
            
            var result = await _classUnderTest.Get("br");

            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo<Country>();
            
        }

        [Fact]
        public async Task Should_return_null_for_country_code_not_found()
        {
            _handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .Returns(Task.FromResult(InValidCountryResponse));

            Should.ThrowAsync<NotFoundException>(() => _classUnderTest.Get("zxz"));
          
        }
        
        [Fact]
        public async Task Should_return_null_for_error_status_code()
        {
            _handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .Returns(Task.FromResult(ErrorCountryResponse));
            
            var result = await _classUnderTest.Get("zxz");
            
            result.ShouldBeNull();
        }
        
        private static HttpResponseMessage ValidCountryResponse => new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("[{\"page\":1,\"pages\":1,\"per_page\":\"50\",\"total\":1},[{\"id\":\"BRA\",\"iso2Code\":\"BR\",\"name\":\"Brazil\",\"region\":{\"id\":\"LCN\",\"iso2code\":\"ZJ\",\"value\":\"LatinAmerica&Caribbean\"},\"adminregion\":{\"id\":\"LAC\",\"iso2code\":\"XJ\",\"value\":\"LatinAmerica&Caribbean(excludinghighincome)\"},\"incomeLevel\":{\"id\":\"UMC\",\"iso2code\":\"XT\",\"value\":\"Uppermiddleincome\"},\"lendingType\":{\"id\":\"IBD\",\"iso2code\":\"XF\",\"value\":\"IBRD\"},\"capitalCity\":\"Brasilia\",\"longitude\":\"-47.9292\",\"latitude\":\"-15.7801\"}]]")
        };

        private static HttpResponseMessage InValidCountryResponse => new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("[{\"message\":[{\"id\":\"120\",\"key\":\"Invalid value\",\"value\":\"The provided parameter value is not valid\"}]}]")
        };
        
        private static HttpResponseMessage ErrorCountryResponse => new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest
        };
    }
}