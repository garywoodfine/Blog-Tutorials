using System;
using System.Runtime.Serialization;

namespace Boleyn.Countries.Content.Exceptions
{
    public class CountryValidationException  : Exception
    {
        public CountryValidationException(string message) : base(message)
        {
            
        }
        protected CountryValidationException(SerializationInfo info,StreamingContext context)
            :base(info,context){}
    }
}