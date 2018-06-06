namespace MyWebServer.Server.Http.Contracts
{
   public interface IHttpContext
    {
        IHttpRequest Request { get; }
    }
}
