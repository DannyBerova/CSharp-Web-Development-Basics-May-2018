namespace HttpServer.Server.Routing.Contracts
{
    using System.Collections.Generic;

    using HttpServer.Server.Enums;

    public interface IServerRouteConfig
    {
        IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }

        ICollection<string> AnonymousPaths { get; }

        ICollection<string> AllowedFolders { get; }
    }
}
