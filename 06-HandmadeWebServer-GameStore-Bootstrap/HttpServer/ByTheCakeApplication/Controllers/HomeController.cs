namespace HttpServer.ByTheCakeApplication.Controllers
{
    using HttpServer.Server.Http;
    using Infrastructures;    
    using Server.Http.Contracts;

    public class HomeController : BaseController
    {
        public HomeController(IHttpRequest req)
            :base(req)
        {

        }

        public IHttpResponse Index()
        {
            return this.FileViewResponse(@"home\index");
        }

        public IHttpResponse About()
        {
            return this.FileViewResponse(@"home\about");
        }
    }
}
