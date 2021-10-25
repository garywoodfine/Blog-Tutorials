using System;
using System.Collections.Generic;

namespace Boleyn.Countries.Content.Exceptions
{
    [Serializable]
    public class ValidationsException : CountriesException
    {
        public ValidationsException(string message, IReadOnlyDictionary<string, string[]> errors) : base(ExceptionName.Validation , message) =>
            Errors = errors;

        public IReadOnlyDictionary<string, string[]> Errors { get; }
    }
}