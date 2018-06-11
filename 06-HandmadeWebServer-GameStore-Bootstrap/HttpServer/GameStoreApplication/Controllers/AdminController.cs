namespace HttpServer.GameStoreApplication.Controllers
{
    using System.Linq;

    using ViewModels.Admin;
    using Services.Contracts;
    using Services;
    using Server.Http.Response;
    using Server.Http.Contracts;
    using System;
    using HttpServer.GameStoreApplication.Common;

    public class AdminController : BaseController
    {
        private const string AddGameView = @"admin/add-game";
        private const string ListGamesView = @"admin/list-games";
        private const string EditGameView = @"admin/edit-game";
        private const string DeleteGameView = @"admin/delete-game";

        private readonly IGameService gameService;

        public AdminController(IHttpRequest req) 
            : base(req)
        {
            this.gameService = new GameService();
        }

        // GET
        public IHttpResponse Add()
        {
            var redirectResponse = this.RedirectIfNotAdminOrNotAthenticated();

            if (redirectResponse != null)
            {
                return redirectResponse;
            }

            return this.FileViewResponse(AddGameView);
        }

        // POST
        public IHttpResponse Add(AdminAddGameViewModel model)
        {
            var redirectResponse = this.RedirectIfNotAdminOrNotAthenticated();

            if (redirectResponse != null)
            {
                return redirectResponse;
            }

            if (!this.ValidateModel(model))
            {
                return this.Add();
            }

            this.gameService.Create(
                model.Title,
                model.Description, 
                model.Image, 
                model.Price, 
                model.Size, 
                model.VideoId, 
                model.ReleaseDate.Value);

            return new RedirectResponse(Routes.ListAllGamesPath);
        }

        // GET
        public IHttpResponse List()
        {
            var redirectResponse = this.RedirectIfNotAdminOrNotAthenticated();

            if(redirectResponse != null)
            {
                return redirectResponse;
            }

            var games = this.gameService.All()
                .Select(g => $"<tr><th scope=\"row\">{g.Id}</th><td>{g.Title}</td><td>{g.Size:F1} GB</td><td>{g.Price:F2} &euro;</td><td><a href=\"/admin/games/edit/{g.Id}\" class=\"btn btn-warning btn-sm\" style=\"margin: auto 4px;\">Edit</a><a href=\"/admin/games/delete/{g.Id}\" class=\"btn btn-danger btn-sm\" style=\"margin: auto 4px;\">Delete</a></td></tr>");

            var gamesAsHtml = string.Join(Environment.NewLine, games);

            this.ViewData["games"] = gamesAsHtml;

            return this.FileViewResponse(ListGamesView);
        }

        // GET
        public IHttpResponse Edit(int id)
        {
            var redirectResponse = this.RedirectIfNotAdminOrNotAthenticated();

            if (redirectResponse != null)
            {
                return redirectResponse;
            }

            var game = this.gameService.FindById(id);

            if(game == null)
            {
                this.ShowError("Game not found!");

                return this.List();
            }

            this.ViewData["title"] = game.Title;
            this.ViewData["description"] = game.Description;
            this.ViewData["imageUrl"] = game.ImageUrl;
            this.ViewData["price"] = game.Price.ToString("F2");
            this.ViewData["size"] = game.Size.ToString("F1");
            this.ViewData["videoId"] = game.VideoId;
            this.ViewData["releaseDate"] = game.ReleaseDate.ToString("yyyy-MM-dd");

            return this.FileViewResponse(EditGameView);
        }

        // POST
        public IHttpResponse Edit(AdminEditGameViewModel model)
        {
            var redirectResponse = this.RedirectIfNotAdminOrNotAthenticated();

            if (redirectResponse != null)
            {
                return redirectResponse;
            }

            if (!this.ValidateModel(model))
            {
                return this.Edit(model.Id);
            }

            bool isEdited = this.gameService.Edit(
                model.Id,
                model.Title,
                model.Description,
                model.Image,
                model.Price,
                model.Size,
                model.VideoId,
                model.ReleaseDate.Value);

            if (!isEdited)
            {
                this.ShowError("Game not found in database");

                return this.Edit(model.Id);
            }

            return new RedirectResponse(Routes.ListAllGamesPath);
        }

        // GET
        public IHttpResponse Delete(int id)
        {
            var redirectResponse = this.RedirectIfNotAdminOrNotAthenticated();

            if (redirectResponse != null)
            {
                return redirectResponse;
            }

            var game = this.gameService.FindById(id);

            if (game == null)
            {
                this.ShowError("Game not found!");

                return this.List();
            }

            this.ViewData["title"] = game.Title;
            this.ViewData["description"] = game.Description;
            this.ViewData["imageUrl"] = game.ImageUrl;
            this.ViewData["price"] = game.Price.ToString("F2");
            this.ViewData["size"] = game.Size.ToString("F1");
            this.ViewData["videoId"] = game.VideoId;
            this.ViewData["releaseDate"] = game.ReleaseDate.ToString("yyyy-MM-dd");

            return this.FileViewResponse(DeleteGameView);
        }

        // POST
        public IHttpResponse Delete(AdminDeleteGameViewModel model)
        {
            var redirectResponse = this.RedirectIfNotAdminOrNotAthenticated();

            if (redirectResponse != null)
            {
                return redirectResponse;
            }

            bool isDeleted = this.gameService.Delete(model.GameId);

            if (!isDeleted)
            {
                this.ShowError("Game not found!");

                return this.Delete(model.GameId);
            }

            return this.RedirectResponse(Routes.ListAllGamesPath);
        }
    }
}
