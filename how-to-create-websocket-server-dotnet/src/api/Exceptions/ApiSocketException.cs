using System;

namespace ApiSocket.Exceptions
{
    public class ApiSocketException : Exception
    {
        public ApiSocketException(string title, string message) : base(message) => Title = title;
        public string Title { get; set; }
    }
}