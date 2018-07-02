namespace ExamWeb.App.Controllers
{
    using Infrastructure;
    using Models.BindingModels;
    using Services;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Attributes.Security;
    using SimpleMvc.Framework.Interfaces;

    public class UsersController : BaseController
    {
        private readonly UsersService users;

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
                SetValidatorErrors();
                return this.View();
            }

            int? position = this.users.GetPosition(model.Position);

            if (position == null)
            {
                this.ShowError(Constants.InvalidPossitionMessage);
                return this.View();
            }

            var userId = this.users.Create(model.Email, model.Password, position);
            if (userId == null)
            {
                ShowError(Constants.UnsuccessfullRegistrationMessage);
                return this.View();
            }

            this.SignIn(model.Email, userId.Value);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            var userId = this.users.UserExists(model.Email, model.Password);

            if (userId == null)
            {
                ShowError(Constants.InvalidCredentialsMessage);
                return this.View();
            }

            this.SignIn(model.Email, userId.Value);
            return this.RedirectToHome();
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Logout()
        {
            if (!this.User.IsAuthenticated)
            {
              return this.RedirectToLogin();
            }

            this.SignOut();
            return this.RedirectToHome();
        }
    }
}
