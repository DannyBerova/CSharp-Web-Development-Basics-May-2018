namespace HttpServer.GameStoreApplication
{
    using Common;
    using Controllers;
    using Data;
    using Data.Seed;
    using Microsoft.EntityFrameworkCore;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using System;
    using System.Globalization;
    using System.Linq;
    using ViewModels.Account;
    using ViewModels.Admin;

    public class GameStoreApp : IApplication
    {
        public void InitalizeDatabase()
        {
            Console.WriteLine("Initializing database...");
            using (var db = new GameStoreDbContext())
            {
                db.Database.Migrate();

                if (!db.Games.Any())
                {
                    var seeder = new Seeder();
                    seeder.SeedGamesInGameStore();
                }
            }
        }

        public void Configure(IAppRouteConfig appRouteConfig)
        {
            const string StylesFolder = "/css";
            const string ScriptsFolder = "/scripts";

            appRouteConfig.AllowedFolders.Add(StylesFolder);
            appRouteConfig.AllowedFolders.Add(ScriptsFolder);

            appRouteConfig.AnonymousPaths.Add(Routes.LoginPath);
            appRouteConfig.AnonymousPaths.Add(Routes.RegisterPath);
            appRouteConfig.AnonymousPaths.Add(Routes.HomePath);
            appRouteConfig.AnonymousPaths.Add(Routes.GameDetailsRegexPath);
            appRouteConfig.AnonymousPaths.Add(Routes.CartPath);
            appRouteConfig.AnonymousPaths.Add(Routes.BuyGameRegexPath);
            appRouteConfig.AnonymousPaths.Add(Routes.CartRemoveItemRegexPath);

            appRouteConfig
                .Get(Routes.HomePath,
                req => new HomeController(req).Index());

            appRouteConfig.Get(Routes.RegisterPath,
                req => new AccountController(req).Register());

            appRouteConfig.Post(Routes.RegisterPath,
                req => new AccountController(req).Register(
                    new RegisterViewModel()
                    {
                        Email = req.FormData["email"],
                        FullName = req.FormData["full-name"],
                        Password = req.FormData["password"],
                        ConfirmPassword = req.FormData["confirm-password"],
                    }));

            appRouteConfig
                .Get(Routes.LoginPath,
                    req => new AccountController(req).Login());

            appRouteConfig
                .Post(Routes.LoginPath,
                    req => new AccountController(req).Login(new LoginViewModel()
                    {
                        Email = req.FormData["email"],
                        Password = req.FormData["password"]
                    }));

            appRouteConfig
                .Get(Routes.LogoutPath,
                    req => new AccountController(req).Logout());

            appRouteConfig
                .Get(Routes.AddGamePath,
                    req => new AdminController(req).Add());

            appRouteConfig
                .Post(Routes.AddGamePath,
                    req => new AdminController(req).Add(
                        new AdminAddGameViewModel()
                        {
                            Title = req.FormData["title"],
                            Description = req.FormData["description"],
                            Image = req.FormData["thumbnail"],
                            Price = decimal.Parse(req.FormData["price"]),
                            Size = double.Parse(req.FormData["size"]),
                            VideoId = req.FormData["video-id"],
                            ReleaseDate = DateTime.ParseExact(
                                req.FormData["release-date"],
                                "yyyy-MM-dd",
                                CultureInfo.InvariantCulture)
                        }));

            appRouteConfig
                .Get(Routes.ListAllGamesPath,
                    req => new AdminController(req).List());

            appRouteConfig
                .Get(Routes.EditGamePath,
                    req => new AdminController(req).Edit(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post(Routes.EditGamePath,
                    req => new AdminController(req).Edit(
                        new AdminEditGameViewModel()
                        {
                            Id = int.Parse(req.UrlParameters["id"]),
                            Title = req.FormData["title"],
                            Description = req.FormData["description"],
                            Image = req.FormData["thumbnail"],
                            Price = decimal.Parse(req.FormData["price"]),
                            Size = double.Parse(req.FormData["size"]),
                            VideoId = req.FormData["video-id"],
                            ReleaseDate = DateTime.ParseExact(
                                req.FormData["release-date"],
                                "yyyy-MM-dd",
                                CultureInfo.InvariantCulture)
                        }));

            appRouteConfig
                .Get(Routes.DeleteGamePath,
                    req => new AdminController(req).Delete(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post(Routes.DeleteGamePath,
                    req => new AdminController(req).Delete(
                        new AdminDeleteGameViewModel()
                        {
                            GameId = int.Parse(req.UrlParameters["id"])
                        }));

            appRouteConfig
                .Get(Routes.GameDetailsPath,
                    req => new GameController(req).Details(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get(Routes.CartPath,
                    req => new CartController(req).Cart());

            appRouteConfig
                .Post(Routes.CartPath,
                    req => new CartController(req).Order());

            appRouteConfig
                .Get(Routes.BuyGamePath,
                    req => new CartController(req).Buy(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get(Routes.CartRemoveItemPath,
                    req => new CartController(req).Remove(int.Parse(req.UrlParameters["id"])));
        }
    }
}
