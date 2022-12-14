using Domain;

namespace Services;

public interface IAddressDataProvider
{
    Task<Source> GetByPostCode(string postcode, CancellationToken cancellationToken);
}