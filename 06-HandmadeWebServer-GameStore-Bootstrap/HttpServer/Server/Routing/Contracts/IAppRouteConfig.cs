namespace HttpServer.Server.Routing.Contracts
{
    using System;
    using System.Collections.Generic;

    using Enums;
    using Handlers;
    using Http.Contracts;

    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes { get; }

        void Get(string route, Func<IHttpRequest, IHttpResponse> handler);

        void Post(string route, Func<IHttpRequest, IHttpResponse> handler);

        void AddRoute(string route, HttpRequestMethod method, RequestHandler httpHandler);

        ICollection<string> AnonymousPaths { get; }

        ICollection<string> AllowedFolders { get; }
    }
}
