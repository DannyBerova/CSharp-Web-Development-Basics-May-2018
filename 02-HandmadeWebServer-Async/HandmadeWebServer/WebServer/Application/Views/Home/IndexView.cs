namespace MyWebServer.Application.Views.Home
{
    using Server.Contracts;
    using System.IO;

    public class IndexView : IView
    {
        string file = File.ReadAllText(@"..\..\..\Application\Views\Home\Index.html");
        public string View() => this.file;

        //        public string View() => @"<h1>Welcome to my first server!</h1>
        //<h2>Welcome to my first server!</h2>";
    }
}
