namespace MyWebServer.Application.Controllers
{
    using Server;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System.Linq;
    using Views.Home;

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
                return this.RegisterGet();
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
                return this.RegisterGet();
            }
            return new ViewResponse(HttpStatusCode.Ok, new UserDetailsView(model));
        }
    }
}
