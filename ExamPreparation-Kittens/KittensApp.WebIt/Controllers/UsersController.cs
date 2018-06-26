namespace KittensApp.WebIt.Controllers
{
    using Models.BindingModels;
    using Services;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Attributes.Security;
    using SimpleMvc.Framework.Interfaces;

    public class UsersController : BaseController
    {
        private UsersService users;

        public UsersController()
        {
            this.users = new UsersService();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisteringModel model)
        {
            if (!this.IsValidModel(model))
            {
                ShowError("Check your form for errors!");
                return this.View();
            }

            var user = this.users.Create(model.Username, model.Password, model.Email);
            if (user == null)
            {
                ShowError("Unsuccessfull Registration!");
                return this.View();
            }

            this.SignIn(user.Username, user.Id);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Login()
        {
            this.Model.Data["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLoggingInModel model)
        {
            var user = this.users.UserExists(model.Username, model.Password);

            if (user == null)
            {
                ShowError("Invalid credentials!");
                return this.View();
            }

            this.SignIn(user.Username, user.Id);
            return this.RedirectToHome();
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Logout()
        {
            this.SignOut();
            return this.RedirectToHome();
        }
    }
}
