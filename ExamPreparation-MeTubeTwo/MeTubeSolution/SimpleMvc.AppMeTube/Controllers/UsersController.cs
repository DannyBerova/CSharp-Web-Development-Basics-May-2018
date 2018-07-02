namespace SimpleMvc.AppMeTube.Controllers
{
    using System.Text;
    using Framework.Attributes.Methods;
    using Framework.Interfaces;
    using Helpers;
    using Infrastructure;
    using Models;
    using Services;

    public class UsersController : BaseController
    {
        //public const string RegisterErrorMessage = @"<p>Check your form for errors.</p><p> Username must be between 3 and 50 symbols.</p><p>Passwords must be between 4 and 30 symbols.</p><p>Confirm password must match the provided password.</p>";
        //public const string ConfirmPasswordError = @"<p>Confirm password must match the provided password.</p>";

        private readonly UsersService users;
        private readonly TubesService tubes;

        public UsersController()
        {
            this.users = new UsersService();
            this.tubes = new TubesService();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }

            // Validator checks for password and confirm-password match itself
            if (!this.IsValidModel(model))
            {
                SetValidatorErrors();
                return this.View();
            }

            //if (model.Password != model.ConfirmPassword)
            //{
            //    ShowError(ConfirmPasswordError);
            //    return View();
            //}

            var userId = this.users.Create(model.Username, model.Password, model.Email);

            if (userId == null)
            {
                ShowError(Constants.UnsuccessfullRegistrationMessage);
                return View();
            }

            SignIn(model.Username, userId.Value);
            return RedirectToHome();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserBindingModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }

            var userId = this.users.UserExists(model.Username, model.Password);

            if (userId == null)
            {
                ShowError(Constants.InvalidCredentialsMessage);
                return this.View();
            }

            this.SignIn(model.Username, userId.Value);
            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }

            var email = this.ProfileDb.Email;
            var tubes = this.tubes.AllByUser(id);
            var builder = new StringBuilder();

            foreach (var tube in tubes)
            {
                builder.AppendLine(tube.ToHtml());
            }

            this.Model["username"] = this.User.Name;
            this.Model["useremail"] = email;
            this.Model["tubes"] = builder.ToString();
            return View();
        }

        [HttpGet]
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
