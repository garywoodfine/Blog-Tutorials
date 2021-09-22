using System;
using System.Runtime.Serialization;

namespace Boleyn.Countries.Content.Exceptions
{
    [Serializable]
    public class CountryNotFoundException : Exception
    {
        public CountryNotFoundException(string message) : base(message)
        {
            
        }
        protected CountryNotFoundException(SerializationInfo info,StreamingContext context)
            :base(info,context){}
    }
}