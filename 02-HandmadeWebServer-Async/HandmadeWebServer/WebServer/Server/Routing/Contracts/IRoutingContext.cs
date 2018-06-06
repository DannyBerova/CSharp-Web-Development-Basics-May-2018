namespace MyWebServer.Server.Routing.Contracts
{
    using Handlers;
    using System.Collections.Generic;

    public interface IRoutingContext
    {
        IEnumerable<string> Parameters { get; }

        RequestHandler RequestHandler { get; }
    }
}
