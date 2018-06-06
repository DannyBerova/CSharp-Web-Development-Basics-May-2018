using MyWebServer.ByTheCakeApp.Infrastructure;
namespace MyWebServer.ByTheCakeApp.Controllers
{
    using Server.Http.Contracts;

    public class HomeController : Controller
    {
        
        public IHttpResponse Index()
        {

            return FileViewResponse(@"home\index");
        }

        public IHttpResponse About()
        {
            return FileViewResponse(@"home\about");
        }
    }
}
