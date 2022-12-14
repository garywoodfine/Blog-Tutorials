using Domain;

namespace Services;

public class AddressProvider : IAddressDataProvider
{
    private readonly HttpClient _httpClient;

    public AddressProvider(HttpClient client)
    {
        _httpClient = client;
    }

    public Task<Source> GetByPostCode(string postcode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}