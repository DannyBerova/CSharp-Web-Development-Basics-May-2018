using MyWebServer.Application.Controllers;
using MyWebServer.Server.Contracts;
using MyWebServer.Server.Routing.Contracts;

namespace MyWebServer.Application
{
    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get(
                "/",
                req => new HomeController().Index());

            appRouteConfig.Get(
                "/testsession",
                req => new HomeController().SessionTest(req));

            appRouteConfig.Get(
                "/register",
                req => new UserController()
                    .RegisterGet());

            appRouteConfig.Post(
                "/register",
                req => new UserController()
                    .RegisterPost(req.FormData["name"]));

            appRouteConfig.Get(
                "/user/{(?<name>[a-z]+)}",
                req => new UserController()
                    .Details(req.UrlParameters["name"]));
        }
    }
}
