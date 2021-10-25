using System;

namespace Boleyn.Countries.Content.Exceptions
{
    public abstract class CountriesException : Exception
    {
        protected CountriesException(string title, string message) : base(message) => Title = title;

        public string Title { get; set; }
    }
}