namespace SoftUni.ChushkaApp.Controllers
{
    using Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Text;
    using WebServer.Mvc.Attributes.HttpMethods;
    using WebServer.Mvc.Interfaces;
    using WebServer.Services;

    public class HomeController : BaseController
    {
        private readonly ProductsService products;
        private readonly UsersService users;

        public HomeController()
        {
            this.products = new ProductsService();
            this.users = new UsersService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.IsAuthenticated)
            {
                var guestFormat = File.ReadAllText(Constants.GuestFormatPath);
                this.ViewData["homeContent"] = guestFormat;
            }
            else
            {

                var userFormat = File.ReadAllText(Constants.UserFormatPath);

                this.ViewData["homeContent"] = userFormat;
                this.ViewData["greetings"] = Constants.UserGreetingsMessage;

                if (this.User.IsInRole("Admin"))
                {
                    this.ViewData["greetings"] = Constants.AdminGreetingsMessage;
                }

                var productsCards = this.products.All().Select(g => g.ToHtml()).ToList();

                var result = new StringBuilder();

                for (int i = 0; i < productsCards.Count; i++)
                {
                    if (i % 5 == 0)
                    {
                        result.Append(@"</div><div class=""row d-flex justify-content-around mt-4"">");
                    }

                    result.Append(productsCards[i]);
                }

                result.Append("</div>");

                this.ViewData.Data["username"] = this.User.Name;
                this.ViewData.Data["products"] = result.ToString();
            }

            return this.View();
        }
    }
}
