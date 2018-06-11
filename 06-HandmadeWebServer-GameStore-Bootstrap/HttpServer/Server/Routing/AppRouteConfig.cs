namespace HttpServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Enums;
    using Handlers;
    using HttpServer.Server.Http.Contracts;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> routes;

        public AppRouteConfig()
        {
            this.AnonymousPaths = new List<string>();
            this.AllowedFolders = new List<string>();

            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>>();

            var availableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, RequestHandler>();
            }
        }

        public IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes => this.routes;

        public ICollection<string> AnonymousPaths { get; private set; }

        public ICollection<string> AllowedFolders { get; private set; }

        public void Get(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            this.AddRoute(route, HttpRequestMethod.Get, new GetHandler(handler));
        }

        public void Post(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            this.AddRoute(route, HttpRequestMethod.Post, new PostHandler(handler));
        }

        public void AddRoute(string route, HttpRequestMethod method,RequestHandler httpHandler)
        {           
            this.routes[method].Add(route, httpHandler);            
        }
    }
}
