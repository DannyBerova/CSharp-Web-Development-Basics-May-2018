namespace HttpServer.GameStoreApplication.Controllers
{
    using Services;
    using Services.Contracts;
    using Server.Http.Contracts;
    using HttpServer.Server.Http.Response;
    using HttpServer.GameStoreApplication.Common;
    using System;
    using System.Collections.Generic;
    using HttpServer.GameStoreApplication.Data.Models;
    using System.Linq;
    using HttpServer.Server.Http;

    public class GameController : BaseController
    {
        private IGameService gameService;

        public GameController(IHttpRequest req) 
            : base(req)
        {
            this.gameService = new GameService();
        }

        // Get
        public IHttpResponse Details(int id)
        {
            var game = this.gameService.FindById(id);

            if(game == null)
            {
                return new RedirectResponse(Routes.HomePath);
            }

            this.ViewData["id"] = game.Id.ToString();
            this.ViewData["title"] = game.Title;
            this.ViewData["videoId"] = game.VideoId;
            this.ViewData["description"] = game.Description;
            this.ViewData["price"] = game.Price.ToString("F2");
            this.ViewData["size"] = game.Size.ToString("F1");
            this.ViewData["releaseDate"] = game.ReleaseDate.ToString("yyyy-MM-dd");


            var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
            var ownedGames = this.gameService.FindByUserEmail(email);

            this.ViewData["ownedGameDisplay"] = ownedGames.Any(g => g.Id == id) ? "none" : "inline-block";

            if (this.Authentication.IsAdmin)
            {
                this.ViewData["adminDisplay"] = "inline-block";
            }

            return this.FileViewResponse("game/game-details");
        }
    }
}
