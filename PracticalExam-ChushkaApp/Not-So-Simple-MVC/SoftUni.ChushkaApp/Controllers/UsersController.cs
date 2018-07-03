namespace SoftUni.ChushkaApp.Controllers
{
    using System.Collections.Generic;
    using Infrastructure;
    using Models;
    using WebServer.Mvc.Attributes.HttpMethods;
    using WebServer.Mvc.Interfaces;
    using WebServer.Services;

    public class UsersController : BaseController
    {
        //public const string RegisterErrorMessage = @"<p>Check your form for errors.</p><p> Username must be between 3 and 50 symbols.</p><p>Passwords must be between 4 and 30 symbols.</p><p>Confirm password must match the provided password.</p>";
        //public const string ConfirmPasswordError = @"<p>Confirm password must match the provided password.</p>";

        private readonly UsersService users;

        private readonly ProductsService products;
        private readonly OrdersService orders;

        public UsersController()
        {
            this.users = new UsersService();
            this.products = new ProductsService();
            this.orders = new OrdersService();
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

            var userId = this.users.Create(model.Username, model.Password, model.FullName, model.Email);

            if (userId == null)
            {
                ShowError(Constants.UnsuccessfullRegistrationMessage);
                return View();
            }

            var role = userId == 1 ? "Admin" : "User";

            this.SignIn(model.Username, userId.Value,  new List<string>(){role});
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

            var role = userId == 1 ? "Admin" : "User";
            this.SignIn(model.Username, userId.Value, new List<string>() { role });
            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Order(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }

            var productId = id;
            var userId = this.User.Id;
            var salePrice = this.products.GetSalePrice(id);

            var success = this.orders.Create(productId, userId, salePrice);

            if (!success)
            {
                ShowError("Unsuccessfull Order!");
            }

            this.ShowAlert("Successfull Order!");
            return RedirectToHome();
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
