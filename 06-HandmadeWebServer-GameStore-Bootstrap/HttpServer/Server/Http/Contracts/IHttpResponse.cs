namespace HttpServer.Server.Http.Contracts
{
    using Server.Enums;

    public interface IHttpResponse
    {
        IHttpHeaderCollection Headers { get; }

        IHttpCookieCollection Cookies { get; }

        HttpStatusCode StatusCode { get; }
    }
}
