using System.Text;

namespace ExamWeb.App.Controllers
{
    using System.IO;
    using System.Linq;
    using Data;
    using ExamWeb.Models;
    using Infrastructure;
    using Services;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;

    public abstract class BaseController : Controller
    {
        private readonly LogsService logs;

        protected BaseController()
        {
            this.Context = new ModPanelDbContext();
            this.logs = new LogsService();
            this.SeedDb();

            this.Model["show-error"] = "none";
            this.Model["adminMenu"] = string.Empty;
            this.Model["homeContent"] = File.ReadAllText(Constants.GuestFormatPath);
        }

        protected ModPanelDbContext Context { get; set; }

        protected User Profile { get; private set; }

        protected bool IsAdmin => this.User.IsAuthenticated && this.Profile.IsAdmin;

        public override void OnAuthentication()
        {

            this.Model["topMenu"] = this.User.IsAuthenticated ? Constants.UsersTopMenuHtml : Constants.GuestsTopMenuHtml;

            if (this.User.IsAuthenticated)
            {
                this.Model["topMenu"] = Constants.UsersTopMenuHtml;

                using (var db = new ModPanelDbContext())
                {
                    this.Profile = db
                        .Users
                        .First(u => u.Email == this.User.Name);

                    if (this.Profile.IsAdmin)
                    {
                        this.Model["adminMenu"] = Constants.AdminOnlyMenu;
                    }
                }
            }
            else
            {
                this.Model["topMenu"] = Constants.GuestsTopMenuHtml;
            }

            base.OnAuthentication();
        }

        public void SeedDb()
        {
            using (this.Context)
            {
                if (!this.Context.Positions.Any())
                {
                    this.Context.Positions.Add(new Position() { Name = "Developer" });
                    this.Context.Positions.Add(new Position() { Name = "Designer" });
                    this.Context.Positions.Add(new Position() { Name = "Technical Support" });
                    this.Context.Positions.Add(new Position() { Name = "Technical Trainer" });
                    this.Context.Positions.Add(new Position() { Name = "HR" });
                    this.Context.Positions.Add(new Position() { Name = "Marketing Specialist" });

                    this.Context.SaveChanges();
                }
            }
        }

        protected IActionResult RedirectToHome() => RedirectToAction("/home/index");

        protected IActionResult RedirectToLogin() => RedirectToAction("/users/login");

        protected void Log(LogType type, string activityInfo)
            => this.logs.Create(
                this.Profile.Email,
                type,
                activityInfo);

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
            this.Model["show-error"] = "block";
            this.Model["error"] = error;
        }
    }
}