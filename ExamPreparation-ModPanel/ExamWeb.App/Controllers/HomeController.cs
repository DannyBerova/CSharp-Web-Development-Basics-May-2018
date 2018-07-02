namespace ExamWeb.App.Controllers
{
    using Infrastructure;
    using Services;
    using Services.ViewModels;
    using SimpleMvc.Framework.Interfaces;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using SimpleMvc.Framework.Attributes.Methods;

    public class HomeController : BaseController
    {
        private readonly PostsService posts;
        private readonly LogsService logs;

        public HomeController()
        {
            this.posts = new PostsService();
            this.logs = new LogsService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.IsAuthenticated)
            {
                var guestFormat = File.ReadAllText(Constants.GuestFormatPath);
                this.Model["homeContent"] = guestFormat;
            }
            else
            {
                var userFormat = File.ReadAllText(Constants.UserFormatPath);

                this.Model["homeContent"] = userFormat;
                //this.Model["username"] = this.User.Name;

                var search = CheckUrlParams();
                var postsData = this.posts.AllWithDetails(search);
                var postsCards = RetrievePostCardsDetails(postsData);
                SetPostsHtmlView(postsCards);

                if (this.Profile.IsAdmin)
                {
                    var adminFormat = File.ReadAllText(Constants.AdminFormatPath);

                    this.Model["homeContent"] = adminFormat;
                    var logsHtml = this.logs
                        .All()
                        .Select(l => l.ToHtml());

                    this.Model["logs"] = string.Join(string.Empty, logsHtml);
                }
            }

            return this.View();
        }

        private string CheckUrlParams()
        {
            string search = null;
            if (this.Request.UrlParameters.ContainsKey("search"))
            {
                search = this.Request.UrlParameters["search"];
            }

            return search;
        }

        private void SetPostsHtmlView(IEnumerable<string> postsCards)
        {
            this.Model["posts"] = postsCards.Any()
                ? string.Join(string.Empty, postsCards)
                : "<h2>No posts found!</h2>";
        }

        private static IEnumerable<string> RetrievePostCardsDetails(IEnumerable<HomeListingModels> postsData)
        {
            return postsData .Select(p => p.ToHtml());
        }
    }
}
