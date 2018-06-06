using System.IO;
using MyWebServer.Server.Contracts;

namespace MyWebServer.Application.Views.Home
{
    public class RegisterView : IView
    {
        string file = File.ReadAllText(@"..\..\..\Application\Views\Home\RegisterSample.html");
        public string View() => this.file;
        //public string View()
        //{
        //    return
        //        "<body>" +
        //        "   <form method=\"POST\">" +
        //        "      Name</br>" +
        //        "      <input type=\"text\" name=\"name\" placeholder=\"Enter name [a-z]\" /><br/>" +
        //        "      <input type=\"submit\" value=\"Submit\"/>" +
        //        "   </form>" +
        //        "</body>";
        //}
    }
}
