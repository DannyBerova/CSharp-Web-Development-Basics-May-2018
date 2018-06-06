namespace MyWebServer.Server.Contracts
{
    using MyWebServer.Server.Routing.Contracts;

    public interface IApplication
    {
        void Configure(IAppRouteConfig appRouteConfig);
    }
}
