using MyWebServer.Server;
using MyWebServer.Server.Contracts;

namespace MyWebServer.Application.Views.Home
{
    public class UserDetailsView : IView
    {
        private readonly Model model;

        public UserDetailsView(Model model)
        {
            this.model = model;
        }

        public string View()
        {
            return
                    "<body>" +
                    $"   Hello, {this.model["name"]}!" +
                    "</br>" +
                    "</br>" +
                    $"<a href=\"http://127.0.0.1:8230\"> GoToHomePage</a>" +
            "</body>";
        }
        //=> $"<body>Hello, {this.model["name"]}!</body>< a href=\"localhost:8230/\">Visit GoToHomePage</a>" ;
    }
}