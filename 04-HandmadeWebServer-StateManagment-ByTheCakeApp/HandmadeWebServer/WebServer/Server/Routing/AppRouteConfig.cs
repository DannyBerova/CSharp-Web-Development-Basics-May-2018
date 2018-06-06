using MyWebServer.Server.Http.Contracts;
using System.Linq;

namespace MyWebServer.Server.Routing
{
    using Contracts;
    using Enums;
    using Handlers;
    using System;
    using System.Collections.Generic;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>>();

            var availableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var requestMethod in availableMethods)
            {
                this.routes[requestMethod] = new Dictionary<string, RequestHandler>();
            }
        }

        public IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes => this.routes;

        public void Get(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            AddRoute(route, HttpRequestMethod.Get, new RequestHandler(handler));
        }

        public void Post(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            AddRoute(route, HttpRequestMethod.Post, new RequestHandler(handler));
        }

        public void AddRoute(string route, HttpRequestMethod method, RequestHandler handler)
        {
            this.routes[method].Add(route, handler);
        }
    }
}
