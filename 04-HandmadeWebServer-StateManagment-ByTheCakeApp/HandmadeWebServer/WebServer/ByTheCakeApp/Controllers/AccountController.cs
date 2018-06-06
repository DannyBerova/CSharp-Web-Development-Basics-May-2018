namespace MyWebServer.ByTheCakeApp.Controllers
{
    using Models;
    using Infrastructure;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;

    public class AccountController : Controller
    {
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "block";
            this.ViewData["authDisplay"] = "none";
            this.ViewData["error"] = "Please, enter data in both fields!";

            return FileViewResponse(@"account\login");
        }

        public IHttpResponse Login(IHttpRequest req)
        {
            const string formNameKey = "name";
            const string formPasswordKey = "password";

            if (!req.FormData.ContainsKey(formNameKey) || 
                !req.FormData.ContainsKey(formPasswordKey))
            {
                this.ViewData["showError"] = "block";
                this.ViewData["authDisplay"] = "none";
                this.ViewData["error"] = "You have empty fields";

                return FileViewResponse(@"account\login");
            }

            var name = req.FormData[formNameKey];
            var password = req.FormData[formPasswordKey];

            
            req.Session.Add(SessionStore.CurrentUserKey, name);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            //return this.FileViewResponse(@"home\index");
            return new RedirectResponse("/");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }
    }
}
