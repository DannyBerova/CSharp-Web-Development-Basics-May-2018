namespace SimpleMvc.AppMeTube.Controllers
{
    using Data;
    using Framework.Controllers;
    using Framework.Interfaces;
    using Infrastructure;
    using Modelss;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class BaseController : Controller
    {
        protected BaseController()
        {
            this.Context = new MeTubeDbContext();

            this.Model["show-error"] = "none";
            this.Model["adminMenu"] = string.Empty;
            this.Model["homeContent"] = File.ReadAllText(Constants.GuestFormatPath);
        }

        protected MeTubeDbContext Context { get; private set; }

        protected User ProfileDb { get; private set; }

        public override void OnAuthentication()
        {

            this.Model["topMenu"] = this.User.IsAuthenticated ? Constants.UsersTopMenuHtml : Constants.GuestsTopMenuHtml;

            if (this.User.IsAuthenticated)
            {
                this.Model["topMenu"] = Constants.UsersTopMenuHtml;

                    this.ProfileDb = this.Context
                        .Users
                        .First(u => u.Username == this.User.Name);

                this.Model["id"] = this.ProfileDb.Id.ToString();
            }
            else
            {
                this.Model["topMenu"] = Constants.GuestsTopMenuHtml;
            }

            base.OnAuthentication();
        }

        protected void SetValidatorErrors()
        {
            var resultErrors = new StringBuilder();
            var errors = this.ParameterValidator.ModelErrors;
            foreach (var error in errors)
            {
                resultErrors.AppendLine($"<p>{string.Join(" ", error.Value)}</p>");
            }

            this.ShowError(resultErrors.ToString());
        }

        protected void ShowError(string error)
        {
            this.Model.Data["show-error"] = "block";
            this.Model.Data["error"] = error;
        }

        //protected void ShowAlert(string alert)
        //{
        //    this.Model.Data["alertDisplay"] = "block";
        //    this.Model.Data["alertMessage"] = alert;
        //}

        protected IActionResult RedirectToHome()
        {
            return this.RedirectToAction("/home/index");
        }

        protected IActionResult RedirectToLogin()
        {
            return this.RedirectToAction("/users/login");
        }

    }
}
