namespace KittensApp.WebIt.Controllers
{
    using System.IO;
    using SimpleMvc.Framework.Interfaces;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (this.User.IsAuthenticated)
            {
                var userFormat = File.ReadAllText("../../../Views/Home/User.html");

                this.Model.Data["homeContent"] = userFormat;
                this.Model.Data["username"] = this.User.Name;
            }
            else
            {
                var guestFormat = File.ReadAllText("../../../Views/Home/Guest.html");
                this.Model.Data["homeContent"] = guestFormat;
            }

            return this.View();
        }
    }
}
