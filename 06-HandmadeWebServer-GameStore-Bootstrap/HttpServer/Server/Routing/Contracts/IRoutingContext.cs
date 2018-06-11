namespace HttpServer.Server.Routing.Contracts
{
    using System.Collections.Generic;

    using HttpServer.Server.Handlers;

    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        RequestHandler Handler { get; }
    }
}
