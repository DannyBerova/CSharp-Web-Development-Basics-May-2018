using MyWebServer.Server.Routing.Contracts;

namespace MyWebServer.Server.Contracts
{
    public interface IApplication
    {
        void Configure(IAppRouteConfig appRouteConfig);
    }
}
