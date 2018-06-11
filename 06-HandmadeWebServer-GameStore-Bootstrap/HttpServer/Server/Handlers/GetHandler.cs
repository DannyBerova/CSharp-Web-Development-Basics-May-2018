namespace HttpServer.Server.Handlers
{
    using System;

    using Http.Contracts;

    public class GetHandler : RequestHandler
    {
        public GetHandler(Func<IHttpRequest, IHttpResponse> handlingFunc) 
            : base(handlingFunc)
        {
        }
    }
}
