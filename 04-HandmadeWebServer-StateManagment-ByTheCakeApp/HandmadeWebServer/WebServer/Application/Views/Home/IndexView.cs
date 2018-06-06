using System.IO;
using MyWebServer.Server.Contracts;

namespace MyWebServer.Application.Views.Home
{
    public class IndexView : IView
    {
        string file = File.ReadAllText(@"..\..\..\Application\Views\Home\Index.html");
        public string View() => this.file;

//        public string View() => @"<h1>Welcome to my first server!</h1>
//<h2>Testing!</h2>";
    }
}
