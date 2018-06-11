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
    using System.Text;

    public class HomeController : BaseController
    {
        private const string HomePath = "home/index";

        private readonly IGameService gameService;

        public HomeController(IHttpRequest req) 
            : base(req)
        {
            this.gameService = new GameService();
        }

        // GET
        public IHttpResponse Index()
        {
            string cardGroup = "<div class=\"card-group\">";
            string cardPattern = File.ReadAllText("../../../GameStoreApplication/Resources/Home/card.html");
            string cardEnd = "</div>";

            var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
            var ownedGames = this.gameService.FindByUserEmail(email);

            IEnumerable<Game> games = null;

            if (this.Request.UrlParameters.ContainsKey("filter") &&
                this.Request.UrlParameters["filter"] == "Owned")
            {
                if (!this.Authentication.IsAthenticated)
                {
                    return this.RedirectResponse(Routes.LoginPath);
                }

                games = ownedGames;
            }
            else
            {
                games = this.gameService.All();
            }

            var gamesAsHtml = games.Select(g => string.Format(cardPattern,
                g.Id,
                string.Format("\'https://i.ytimg.com/vi/{0}/maxresdefault.jpg\'", g.VideoId),
                g.ImageUrl,
                g.Title,
                g.Price,
                g.Size,
                g.Description.Length > 299 ? g.Description.Substring(0, Math.Min(300, g.Description.Length)) + " ...": g.Description,
                this.Authentication.IsAdmin ? "inline-block" : "none",
                ownedGames.Any(og => og.Id == g.Id) ? "none" : "inline-block"
                )).ToArray();

            StringBuilder builder = new StringBuilder();

            for (int group = 0; group < gamesAsHtml.Length; group += 3)
            {
                builder.Append(cardGroup);

                builder.Append(string.Join(Environment.NewLine, gamesAsHtml, group, Math.Min(3, gamesAsHtml.Length - group)));

                builder.Append(cardEnd);
            }
            
            this.ViewData["games"] = builder.ToString();

            return this.FileViewResponse(HomePath);
        }
    }
}
