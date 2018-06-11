namespace HttpServer.ByTheCakeApplication.Controllers
{
    using HttpServer.Infrastructures;
    using HttpServer.Server.Http.Contracts;

    public abstract class BaseController : Controller
    {
        public BaseController(IHttpRequest req) : base(req)
        {
        }

        protected override string ApplicationDirectory => "ByTheCakeApplication";
    }
}
