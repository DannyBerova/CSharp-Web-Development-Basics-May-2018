namespace HttpServer.ByTheCakeApplication
{
    using Microsoft.EntityFrameworkCore;

    using Controllers;
    using Data;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class ByTheCakeApp : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            const string LoginRoute = "/login";
            const string RegisterRoute = "/register";
            const string AboutRoute = "/about";
            const string HomeRoute = "/";

            appRouteConfig.AnonymousPaths.Add(LoginRoute);
            appRouteConfig.AnonymousPaths.Add(RegisterRoute);
            appRouteConfig.AnonymousPaths.Add(AboutRoute);
            appRouteConfig.AnonymousPaths.Add(HomeRoute);
            
            const string StylesFolder = "/Styles";
            const string ScriptsFolder = "/Scripts";

            appRouteConfig.AllowedFolders.Add(StylesFolder);
            appRouteConfig.AllowedFolders.Add(ScriptsFolder);

            appRouteConfig
                .Get("/", req => new HomeController(req).Index());

            appRouteConfig
                .Get("/about", req => new HomeController(req).About());

            appRouteConfig
                .Get("/profile", req => new AccountController(req).Profile(req));

            appRouteConfig
                .Get("/add", req => new CakesController(req).Add());

            appRouteConfig
                .Post("/add", req => new CakesController(req) .Add(req));

            appRouteConfig
                .Get("/search" , req => new CakesController(req).Search(req));

            appRouteConfig
                .Get("/orders", req => new CakesController(req).Orders(req));

            appRouteConfig
                .Get("cakes/orderDatails/{(?<id>[0-9]+)}", req => new CakesController(req).OrderDetails(req));

            appRouteConfig
                .Get("/cakes/details/{(?<id>[0-9]+)}", req => new CakesController(req).CakeDetails(req));

            appRouteConfig
                .Get("/register", req => new AccountController(req).Register());

            appRouteConfig
                .Post("/register", req => new AccountController(req).Register(req));

            appRouteConfig
                .Get("/login", req => new AccountController(req).Login());

            appRouteConfig
                .Post("/login", req => new AccountController(req).Login(req));

            appRouteConfig
                .Post("/logout", req => new AccountController(req).Logout(req));

            appRouteConfig
                .Get("/shopping/add/{(?<id>[0-9]+)}", req => new ShoppingController(req).AddToCart(req));

            appRouteConfig
                .Get("/cart", req => new ShoppingController(req).ShowCart(req));

            appRouteConfig
                .Post("/shopping/finish-order", req => new ShoppingController(req).FinishOrder(req));
        }

        public void InitalizeDatabase()
        {
            using (var context = new ByTheCakeContext())
            {
                context.Database.Migrate();
            }            
        }
    }
}
