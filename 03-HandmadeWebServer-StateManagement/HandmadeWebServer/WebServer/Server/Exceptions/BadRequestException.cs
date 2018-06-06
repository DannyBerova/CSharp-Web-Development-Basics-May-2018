namespace MyWebServer.Server.Exceptions
{
    using System;

    public class BadRequestException : Exception
    {
        private const string InvalidRequestMessage = "Invalid request line";

        public static object ThrowFromInvalidRequest() => throw new BadRequestException(InvalidRequestMessage);

        public BadRequestException(string message)
        : base(message)
        {
        }
    }
}
