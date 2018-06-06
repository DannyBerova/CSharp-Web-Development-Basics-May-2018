namespace MyWebServer.Server.Http.Contracts
{
    using Enums;

    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }

        IHttpHeaderCollection Headers { get; }

        IHttpCookieCollection Cookies { get; }

        // void AddHeader(string location, string redirectUrl);
    }
}
