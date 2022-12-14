using Common;
using Domain;
using Newtonsoft.Json;

namespace Services;

public class AddressProvider : IAddressDataProvider
{
    private readonly HttpClient _httpClient;

    public AddressProvider(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<Source?> GetByPostCode(string postcode, CancellationToken cancellationToken)
    {
      

    
        var response =
            await _httpClient?.GetAsync(RequestPath(postcode), cancellationToken)!;
        response.EnsureSuccessStatusCode();

        return JsonConvert.DeserializeObject<Source>(
            await response.Content.ReadAsStringAsync(cancellationToken));
    }

    private string RequestPath(string postcode)
    {
        var query = new AfdParameterBuilder()
            .Create(_httpClient?.BaseAddress?.Query)
            .Lookup(postcode)
            .Build();

        var updateRequested = new UriBuilder(_httpClient?.BaseAddress?.ToString()!)
        {
            Query = query
        };

        return updateRequested.Uri.ToString();

    }
}