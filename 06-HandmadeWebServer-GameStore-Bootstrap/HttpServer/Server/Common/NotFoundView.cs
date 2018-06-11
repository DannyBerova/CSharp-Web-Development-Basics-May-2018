namespace HttpServer.Server.Common
{
    using Contracts;

    public class NotFoundView : IView
    {
        public string View()
        {
            return "<h1>404 not found! This page does not exist :(</h1>";
        }
    }
}
