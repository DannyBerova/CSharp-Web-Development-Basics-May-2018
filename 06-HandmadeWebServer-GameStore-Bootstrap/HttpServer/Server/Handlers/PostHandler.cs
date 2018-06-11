namespace HttpServer.Server.Handlers
{
    using System;

    using HttpServer.Server.Http.Contracts;

    public class PostHandler : RequestHandler
    {
        public PostHandler(Func<IHttpRequest, IHttpResponse> handlingFunc) 
            : base(handlingFunc)
        {
        }
    }
}
