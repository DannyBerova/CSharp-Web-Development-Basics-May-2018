namespace SoftUni.ChushkaApp.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using Infrastructure;
    using WebServer.Data;
    using WebServer.Models;
    using WebServer.Mvc.Controllers;
    using WebServer.Mvc.Interfaces;

    public class BaseController : Controller
    {
        protected BaseController()
        {
            this.Context = new ChushkaDbContext();
            //this.SeedDb();

            this.ViewData["show-error"] = "none";
            this.ViewData["show-alert"] = "none";
            this.ViewData["homeContent"] = File.ReadAllText(Constants.GuestFormatPath);
        }

        protected ChushkaDbContext Context { get; set; }

        protected bool IsAdmin => this.User.IsAuthenticated && this.User.Roles.Contains("Admin");

        //public void SeedDb()
        //{
        //    using (this.Context)
        //    {
        //        if (!this.Context.Roles.Any())
        //        {
        //            this.Context.Roles.Add(new Role() { Name = "Admin" });
        //            this.Context.Roles.Add(new Role() { Name = "User" });

        //            this.Context.SaveChanges();
        //        }
        //        if (!this.Context.ProductTypes.Any())
        //        {
        //            this.Context.ProductTypes.Add(new ProductType() { Name = "Food" });
        //            this.Context.ProductTypes.Add(new ProductType() { Name = "Domestic" });
        //            this.Context.ProductTypes.Add(new ProductType() { Name = "Health" });
        //            this.Context.ProductTypes.Add(new ProductType() { Name = "Cosmetic" });
        //            this.Context.ProductTypes.Add(new ProductType() { Name = "Other" });

        //            this.Context.SaveChanges();
        //        }
        //    }
        //}

        public override void OnAuthentication()
        {

            this.ViewData["topMenu"] = this.User.IsAuthenticated ? Constants.UsersTopMenuHtml : Constants.GuestsTopMenuHtml;

            if (this.User.IsAuthenticated)
            {
                this.ViewData["topMenu"] = Constants.UsersTopMenuHtml;

                if (this.User.Roles.Contains("Admin"))
                {
                    this.ViewData["topMenu"] = Constants.AdminOnlyMenu;
                }
            }
            else
            {
                this.ViewData["topMenu"] = Constants.GuestsTopMenuHtml;
            }

            base.OnAuthentication();
        }

        protected IActionResult RedirectToHome() => RedirectToAction("/home/index");

        protected IActionResult RedirectToLogin() => RedirectToAction("/users/login");

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
            this.ViewData["show-error"] = "block";
            this.ViewData["error"] = error;
        }

        protected void ShowAlert(string alert)
        {
            this.ViewData["show-alert"] = "block";
            this.ViewData["alert"] = alert;
        }
    }
}
