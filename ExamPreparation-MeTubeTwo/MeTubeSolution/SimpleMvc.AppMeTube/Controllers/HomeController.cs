namespace SimpleMvc.AppMeTube.Controllers
{
    using Framework.Attributes.Methods;
    using Framework.Interfaces;
    using Helpers;
    using Infrastructure;
    using Services;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class HomeController : BaseController
    {
        private readonly TubesService tubes;
        private readonly UsersService users;

        public HomeController()
        {
            this.tubes = new TubesService();
            this.users = new UsersService();
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

                var tubeCards = this.tubes.All().Select(g => g.ToHtml()).ToList();

                var result = new StringBuilder();

                for (int i = 0; i < tubeCards.Count; i++)
                {
                    if (i % 3 == 0)
                    {
                        result.Append(@"<div class=""card-group col-12 justify-content-left"">");
                    }

                    result.Append(tubeCards[i]);

                    if (i % 3 == 2 || i == tubeCards.Count - 1)
                    {
                        result.Append("</div>");
                    }
                }
                    this.Model.Data["username"] = this.User.Name;
                    this.Model.Data["userhome"] = result.ToString();
            }

            return this.View();
        }
    }
}

