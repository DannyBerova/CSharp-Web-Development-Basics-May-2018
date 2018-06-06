using System.Linq;
using MyWebServer.Application.Views.Home;
using MyWebServer.Server;
using MyWebServer.Server.Enums;
using MyWebServer.Server.Http.Contracts;
using MyWebServer.Server.Http.Response;

namespace MyWebServer.Application.Controllers
{
    public class UserController
    {
        public IHttpResponse RegisterGet()
        {
            return new ViewResponse(HttpStatusCode.Ok, new RegisterView());
        }

        public IHttpResponse RegisterPost(string name)
        {
            if (string.IsNullOrWhiteSpace(name) 
                || name.ToLower() != name 
                || name.ToCharArray().Any(l => l < 97 || l > 122))
            {
                return RegisterGet();
            }
            else
            {
                
            return new RedirectResponse($"/user/{name}");
            }
        }

        public IHttpResponse Details(string name)
        {
            Model model = new Model { ["name"] = name };

            if (name == string.Empty)
            {
                return RegisterGet();
            }
            return new ViewResponse(HttpStatusCode.Ok, new UserDetailsView(model));
        }
    }
}
