namespace HttpServer.GameStoreApplication.Controllers
{
    using HttpServer.GameStoreApplication.Common;
    using Infrastructures;
    using Server.Http.Contracts;

    public abstract class BaseController : Controller
    {
        public BaseController(IHttpRequest req) 
            : base(req)
        {
        }

        protected override string ApplicationDirectory => "GameStoreApplication";
        
        protected IHttpResponse RedirectIfNotAdminOrNotAthenticated()
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(Routes.HomePath);
            }

            if (!this.Authentication.IsAthenticated)
            {
                return this.RedirectResponse(Routes.LoginPath);
            }

            return null;
        }

        protected IHttpResponse RedirectIfUserIsAuthenticated()
        {
            if (this.Authentication.IsAthenticated)
            {
                return this.RedirectResponse(Routes.HomePath);
            }

            return null;
        }
    }
}
