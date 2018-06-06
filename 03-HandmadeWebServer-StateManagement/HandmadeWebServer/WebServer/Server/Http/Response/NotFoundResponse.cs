using MyWebServer.Server.Common;

namespace MyWebServer.Server.Http.Response
{
    using Enums;

    public class NotFoundResponse : ViewResponse
    {
        public NotFoundResponse()
        :base(HttpStatusCode.NotFound, new NotFoundView())
        {
            //this.StatusCode = Enums.HttpStatusCode.NotFound;
        }
    }
}
