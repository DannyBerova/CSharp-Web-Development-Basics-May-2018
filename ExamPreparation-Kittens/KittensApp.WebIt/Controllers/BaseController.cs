namespace KittensApp.WebIt.Controllers
{
    using System.Linq;
    using System.Text;
    using KittenApp.Data;
    using KittenApp.Models;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;

    public abstract class BaseController : Controller
    {
        private const string UsersTopMenuHtml = @"
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/"">Home</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/kittens/all"">All Kittens</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/kittens/add"">Add Kitten</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/users/logout"">Logout</a>
                </li>";
        private const string GuestsTopMenuHtml = @"
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/"">Home</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/users/login"">Login</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/users/register"">Register</a>
                </li>";

        protected BaseController()
        {
            this.Context = new KittenAppContext();
            this.Model.Data["show-error"] = "none";
            SeedDb();
        }

        protected KittenAppContext Context { get; set; }

        public void SeedDb()
        {
            if (!this.Context.Breeds.Any())
            {
                this.Context.Breeds.Add(new Breed() { Name = "Street Transcended" });
                this.Context.Breeds.Add(new Breed() { Name = "American Shorthair" });
                this.Context.Breeds.Add(new Breed() { Name = "Munchkin" });
                this.Context.Breeds.Add(new Breed() { Name = "Siamese" });

                this.Context.SaveChanges();
            }
        }

        public override void OnAuthentication()
        {
            this.Model.Data["topMenu"] = this.User.IsAuthenticated ?  UsersTopMenuHtml : GuestsTopMenuHtml;

            base.OnAuthentication();
        }

        protected IActionResult RedirectToHome() => RedirectToAction("/home/index");
		
        protected void SetValidatorErrors()
        {
            var resultErrors = new StringBuilder();
            var errors = this.ParameterValidator.ModelErrors;
            foreach (var error in errors)
            {
                resultErrors.AppendLine($"<p>{string.Join(" ", error.Value)}</p>");
            }
            this.Model.Data["show-error"] = "block";
            this.Model.Data["error"] = resultErrors.ToString();
        }

        protected void ShowError(string error)
        {
            this.Model.Data["show-error"] = "block";
            this.Model.Data["error"] = error;
        }

    }
}
