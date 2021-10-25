using System;
using System.Runtime.Serialization;

namespace Boleyn.Countries.Content.Exceptions
{
    [Serializable]
    public class NotFoundException : CountriesException
    {
        public NotFoundException(string message) : base( ExceptionName.NotFound, message)
        {
            
        }
    }
}