namespace MyWebServer.Application.Views.Home
{
    using Server;
    using Server.Contracts;

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
    }
}