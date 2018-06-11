namespace HttpServer.ByTheCakeApplication.Controllers
{
    using System;
    using System.Linq;

    using Infrastructures;
    using Server.Http.Response;
    using Server.Http.Contracts;
    using Server.Http;
    using Models;
    using Utilities;
    using HttpServer.ByTheCakeApplication.Data;
    using Microsoft.EntityFrameworkCore;

    public class AccountController : BaseController
    {
        public AccountController(IHttpRequest req)
            :base(req)
        {

        }

        // GET
        public IHttpResponse Register()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authenticatedDisplay"] = "none";

            return this.FileViewResponse("account/register");
        }

        // Post
        public IHttpResponse Register(IHttpRequest request)
        {
            const string formNameKey = "name";
            const string formUsernameKey = "username";
            const string formPasswordKey = "password";
            const string formConfirmPasswordKey = "confirm-password";

            if (!request.FormData.ContainsKey(formNameKey) || 
                !request.FormData.ContainsKey(formUsernameKey) ||
                !request.FormData.ContainsKey(formPasswordKey) ||
                !request.FormData.ContainsKey(formConfirmPasswordKey))
            {
                return new BadRequestResponse();
            }

            string name = request.FormData[formNameKey];
            string username = request.FormData[formUsernameKey];
            string password = request.FormData[formPasswordKey];
            string confirmPassword = request.FormData[formConfirmPasswordKey];

            if((string.IsNullOrEmpty(name) || name.Length < 3) ||
               (string.IsNullOrEmpty(username) || username.Length < 3) ||
               string.IsNullOrEmpty(password) ||
               string.IsNullOrEmpty(confirmPassword) || 
               password != confirmPassword)
            {
                return new RedirectResponse("/register");
            }

            User user = null;            
            using (var context = new ByTheCakeContext())
            {
                if(context.Users.Any(u => u.Username == username))
                {
                    return new RedirectResponse("/register");
                }

                user = new User()
                {
                    Name = name,
                    Username = username,
                    PasswordHash = PasswordUtilities.ComputeHash(password),
                    RegistrationDate = DateTime.UtcNow
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            return CompleteLogin(request, user.Id);
        }

        // GET
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authenticatedDisplay"] = "none";

            return this.FileViewResponse("account/login");
        }

        // Post
        public IHttpResponse Login(IHttpRequest req)
        {
            const string formNameKey = "name";
            const string formPasswordKey = "password";

            if (!req.FormData.ContainsKey(formNameKey) || !req.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            var name = req.FormData["name"];
            var password = req.FormData["password"];

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["error"] = "You have empty fields";
                this.ViewData["showError"] = "block";

                return this.FileViewResponse("account/login");
            }

            User dbUser = null;
            using (var context = new ByTheCakeContext())
            {
                dbUser = context.Users.FirstOrDefault(user => user.Username == name);
            }

            string passwordHash = PasswordUtilities.ComputeHash(password);

            if (dbUser == null || dbUser.PasswordHash != passwordHash)
            {
                this.ViewData["error"] = "Unsuccessful login!";
                this.ViewData["showError"] = "block";
                this.ViewData["authenticatedDisplay"] = "none";

                return this.FileViewResponse("account/login");
            }

            return CompleteLogin(req, dbUser.Id);
        }

        // Post
        internal IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }

        // Get
        public IHttpResponse Profile(IHttpRequest req)
        {
            int currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);

            using (var context = new ByTheCakeContext())
            {
                var currentUser = context.Users.Include(u => u.Orders).FirstOrDefault(u => u.Id == currentUserId);

                if (currentUser == null)
                {
                    return new RedirectResponse("/login");
                }

                this.ViewData["name"] = currentUser.Name;
                this.ViewData["registrationDate"] = currentUser.RegistrationDate.ToString("dd-MM-yyyy");
                this.ViewData["ordersCount"] = currentUser.Orders.Count().ToString();
            }
            return this.FileViewResponse("account/profile");
        }

        private static IHttpResponse CompleteLogin(IHttpRequest req, int userId)
        {
            req.Session.Add(SessionStore.CurrentUserKey, userId);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse("/");
        }
    }
}
