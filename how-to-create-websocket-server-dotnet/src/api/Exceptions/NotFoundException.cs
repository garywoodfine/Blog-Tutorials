using System;


namespace ApiSocket.Exceptions
{
    [Serializable]
    public class NotFoundException : ApiSocketException
    {
        public NotFoundException(string title, string message) : base(title, message)
        {
        }
    }
}