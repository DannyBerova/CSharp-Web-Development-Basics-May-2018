using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Exceptions
{
    public class InvalidResponseException : Exception
    {
        public InvalidResponseException(string message) : base(message)
        {
        }
    }
}
