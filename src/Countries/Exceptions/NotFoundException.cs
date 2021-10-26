using System;
using System.Runtime.Serialization;
using Boleyn.Countries.Resources;

namespace Boleyn.Countries.Content.Exceptions
{
    [Serializable]
    public class NotFoundException : CountriesException
    {
        public NotFoundException(string message) : base(ExceptionTitle.NotFound, message)
        {
            
        }
    }
}