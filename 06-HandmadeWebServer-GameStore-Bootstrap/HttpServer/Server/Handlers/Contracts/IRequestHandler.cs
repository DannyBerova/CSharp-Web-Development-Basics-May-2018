namespace HttpServer.Server.Handlers.Contracts
{
    using HttpServer.Server.Http.Contracts;

    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext httpContext);
    }
}
