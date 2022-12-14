using System.Text;

namespace Common;

/// <summary>
///     Simple builder implementation to build the URL string required for the AFD API Parameters
/// </summary>
public class AfdParameterBuilder
{
    private StringBuilder? _parameterString;
 
    public AfdParameterBuilder Create(string? baseQuery = default)
    {
        _parameterString = string.IsNullOrEmpty(baseQuery) ? new StringBuilder(baseQuery?.Substring(1, baseQuery.Length)) : new StringBuilder();
        return this;
    }

   
    public AfdParameterBuilder Data(string data)
    {
        ConcatParameter( $"{nameof(data)}={data}");
        return this;
    }

    public AfdParameterBuilder CountryCode(string countryISO)
    {
        ConcatParameter( $"{nameof(countryISO)}={countryISO}");
        return this;
    }

    public AfdParameterBuilder Serial(string serial)
    {
        ConcatParameter($"{nameof(serial)}={serial}");
        return this;
    }

    public AfdParameterBuilder Password(string password)
    {
        ConcatParameter($"{nameof(password)}={password}");
        return this;
    }
    public AfdParameterBuilder Task(string task)
    {
        ConcatParameter($"{nameof(task)}={task}");
        return this;
    }
    
    public AfdParameterBuilder Format(string format)
    {
        ConcatParameter($"{nameof(format)}={format}");
        return this;
    }
    public AfdParameterBuilder Lookup(string lookup)
    {
        ConcatParameter($"{nameof(lookup)}={lookup}");
        return this;
    }
    public AfdParameterBuilder Fields(string fields)
    {
        ConcatParameter($"{nameof(fields)}={fields}");
        return this;
    }
  
    public string Build()
    {
        return _parameterString?.ToString() ?? string.Empty;
    }

    /// <summary>
    /// Check if we are the start of the string to evaluate whether we need to append a an ampersand
    /// to string or not. 
    /// </summary>
    /// <param name="param"></param>
    private void ConcatParameter(string param)
    {
        _parameterString?.Append(_parameterString.Length.Equals(0) ? param : string.Concat("&", param));
    }
}