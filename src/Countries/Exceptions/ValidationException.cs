using System;
using System.Collections.Generic;
using Boleyn.Countries.Resources;

namespace Boleyn.Countries.Content.Exceptions
{
    [Serializable]
    public class ValidationException : CountriesException
    {
        public ValidationException(string message, IReadOnlyDictionary<string, string[]> errors) : base(ExceptionTitle.Validation , message) =>
            Errors = errors;

        public IReadOnlyDictionary<string, string[]> Errors { get; }
    }
}