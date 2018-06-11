namespace HttpServer.GameStoreApplication.Controllers
{
    using Common;
    using Data.Models;
    using Server.Http;
    using Server.Http.Contracts;
    using Services;
    using Services.Contracts;

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CartController : BaseController
    {
        private const string Orders = "orders";

        private IGameService gameService;
        private IUserService userService;

        public CartController(IHttpRequest req) 
            : base(req)
        {
            this.gameService = new GameService();
            this.userService = new UserService();
        }

        public IHttpResponse Cart()
        {
            string gamePattern = File.ReadAllText("../../../GameStoreApplication/Resources/Account/cart-game.html");

            var games = this.Request.Session.Get<List<Game>>(Orders);

            if (games != null)
            {
                var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
                var ownedGames = this.gameService.FindByUserEmail(email);

                games.RemoveAll(g => ownedGames.Any(og => og.Id == g.Id));

                var gamesAsHtml = games.Select(g => string.Format(gamePattern,
                        g.Id,
                        g.ImageUrl,
                        g.Title,
                        g.Description,
                        g.Price.ToString("F2")
                    ));

                this.ViewData["games"] = string.Join(Environment.NewLine, gamesAsHtml);
                this.ViewData["totalPrice"] = games.Sum(g => g.Price).ToString("F2");
            }
            else
            {
                this.ViewData["games"] = "You have no games in your cart.";
                this.ViewData["totalPrice"] = "0.00";
            }

            return this.FileViewResponse("account/cart");
        }

        public IHttpResponse Buy(int gameId)
        {
            var game = this.gameService.FindById(gameId);

            if (game == null)
            {
                this.ShowError("Game not found!");
                return this.FileViewResponse("/");
            }

            if (!this.Request.Session.Contains(Orders))
            {
                this.Request.Session.Add(Orders, new List<Game>());
            }

            if (!this.Request.Session.Get<List<Game>>(Orders).Any(g => g.Id == game.Id))
            {
                var userEmail = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);

                if(userEmail != null)
                {
                    var ownedGames = this.gameService.FindByUserEmail(userEmail);

                    if(ownedGames.Any(g => g.Id == game.Id))
                    {
                        return this.RedirectResponse(Routes.HomePath + "?filter=Owned");
                    }
                }

                this.Request.Session.Get<List<Game>>(Orders).Add(game);
            }
            else
            {
                this.ShowError("You already have this game in the shopping cart.");

                return this.RedirectResponse(Routes.CartPath);
            }

            return this.RedirectResponse(Routes.HomePath);
        }

        public IHttpResponse Remove(int id)
        {
            var games = this.Request.Session.Get<List<Game>>(Orders);

            var gameToRemove = games.FirstOrDefault(g => g.Id == id);

            if(gameToRemove == null)
            {
                this.ShowError("This game is not in your shopping cart.");
            }
            else
            {
                games.Remove(gameToRemove);
            }

            return this.RedirectResponse(Routes.CartPath);
        }

        public IHttpResponse Order()
        {
            if (!this.Authentication.IsAthenticated)
            {
                return this.RedirectResponse(Routes.LoginPath);
            }

            var games = this.Request.Session.Get<List<Game>>(Orders);

            if (games == null || !games.Any())
            {
                return this.RedirectResponse(Routes.HomePath);
            }

            var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);

            var user = this.userService.FindByEmail(email);

            var userGames = games.Select(g => new UserGame()
            {
                GameId = g.Id,
                UserId = user.Id
            }).ToList();

            this.gameService.AddRange(userGames);

            games.Clear();

            return this.RedirectResponse(Routes.LoginPath);
        }
    }
}
