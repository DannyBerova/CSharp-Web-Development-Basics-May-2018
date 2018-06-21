namespace SimpleMvc.App.Controllers
{
    using SimpleMVC.Framework.Attributes.Methods;
    using SimpleMVC.Framework.Interfaces;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
