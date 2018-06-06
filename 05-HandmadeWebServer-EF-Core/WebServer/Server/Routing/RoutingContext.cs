namespace MyWebServer.Server.Routing
{
    using Common;
    using Contracts;
    using Handlers;
    using System.Collections.Generic;


    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(RequestHandler handler, IEnumerable<string> parameters)
        {
            CoreValidator.ThrowIfNull(handler, nameof(handler));
            CoreValidator.ThrowIfNull(parameters, nameof(parameters));

            this.RequestHandler = handler;
            this.Parameters = parameters;
        }

        public IEnumerable<string> Parameters { get; private set; }
        public RequestHandler RequestHandler { get; private set; }
    }
}
