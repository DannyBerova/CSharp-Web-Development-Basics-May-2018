namespace MyWebServer.ByTheCakeApp
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class ByTheCakeApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/about", req => new HomeController().About());

            appRouteConfig
                .Get("/add", req => new CakesController().Add());

            appRouteConfig
                .Post("/add", req => new CakesController().Add(req));

            appRouteConfig
                .Get("/search", req => new CakesController().Search(req));

            //appRouteConfig
            //    .Post("/search", req => new CakesController().Search(req.UrlParameters));

            appRouteConfig
                .Get("/login", req => new AccountController().Login());

            appRouteConfig
                .Post("/login", req => new AccountController().Login(req));

            appRouteConfig
                .Post(
                    "/logout", req => new AccountController().Logout(req));

            appRouteConfig
                .Get(
                    "/shopping/add/{(?<id>[0-9]+)}",
                    req => new ShoppingController().AddToCart(req));

            appRouteConfig
                .Get(
                    "/cart",
                    req => new ShoppingController().ShowCart(req));

            appRouteConfig
                .Post(
                    "/shopping/finish-order",
                    req => new ShoppingController().FinishOrder(req));



        }
    }
}
