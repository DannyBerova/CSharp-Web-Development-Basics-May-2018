namespace SimpleMvc.App.Controllers
{
    using SimpleMVC.Framework.Controllers;
    using SimpleMVC.Framework.Interfaces;

    public class BaseController : Controller
    {
        protected BaseController()
        {
            this.Model["anonymousDisplay"] = "flex";
            this.Model["displayAddNote"] = "none";
            this.Model["show-error"] = "none";
        }
        protected void ShowError(string error)
        {
            this.Model["show-error"] = "block";
            this.Model["error"] = error;
        }

        protected IActionResult RedirectToHome()
        {
            return this.RedirectToAction("/");
        }

        protected IActionResult RedirectToLogin()
        {
            return this.RedirectToAction("/user/login");
        }
    }
}
    