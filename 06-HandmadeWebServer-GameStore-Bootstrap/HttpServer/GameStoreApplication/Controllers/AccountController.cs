namespace HttpServer.GameStoreApplication.Controllers
{
    using Services;
    using Services.Contracts;
    using ViewModels.Account;
    using Server.Http.Contracts;
    using Server.Http;
    using Server.Http.Response;
    using System;
    using HttpServer.GameStoreApplication.Common;
    using HttpServer.GameStoreApplication.Data.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class AccountController : BaseController
    {
        private readonly IUserService userService;

        public AccountController(IHttpRequest req) 
            : base(req)
        {
            this.userService = new UserService();
        }

        // GET
        public IHttpResponse Register()
        {
            var redirectResponse = this.RedirectIfUserIsAuthenticated();

            if (redirectResponse != null)
            {
                return redirectResponse;
            }

            return this.FileViewResponse(Routes.RegisterPath);
        }
        
        // POST
        public IHttpResponse Register(RegisterViewModel model)
        {
            var redirectResponse = this.RedirectIfUserIsAuthenticated();

            if (redirectResponse != null)
            {
                return redirectResponse;
            }

            if (!this.ValidateModel(model))
            {
                return this.Register();
            }

            var success = this.userService.Create(model.Email, model.FullName, model.Password);

            if (!success)
            {
                this.ShowError("Email is taken.");

                return this.Register();
            }

            Request.Session.Add(SessionStore.CurrentUserKey, model.Email);

            return this.RedirectResponse(Routes.HomePath);
        }

        // GET
        public IHttpResponse Login()
        {
            var redirectResponse = this.RedirectIfUserIsAuthenticated();

            if (redirectResponse != null)
            {
                return redirectResponse;
            }

            return this.FileViewResponse(Routes.LoginPath);
        }

        // POST
        public IHttpResponse Login(LoginViewModel model)
        {
            var redirectResponse = this.RedirectIfUserIsAuthenticated();

            if (redirectResponse != null)
            {
                return redirectResponse;
            }

            if (!this.ValidateModel(model))
            {
                this.ShowError("Invalid Username or Password.");
                return this.Login();
            }

            var success = this.userService.Find(model.Email, model.Password);

            if (!success)
            {
                this.ShowError("Invalid Username or Password.");
                return this.Login();
            }

            this.LoginUser(model.Email);
            return this.RedirectResponse(Routes.HomePath);
        }

        public IHttpResponse Logout()
        {
            this.Request.Session.Clear();

            return this.RedirectResponse(Routes.HomePath);
        }

        private void LoginUser(string email)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, email);
        }
    }
}
