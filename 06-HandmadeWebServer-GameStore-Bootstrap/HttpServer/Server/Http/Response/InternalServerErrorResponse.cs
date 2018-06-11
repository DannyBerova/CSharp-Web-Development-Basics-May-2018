namespace HttpServer.Server.Http.Response
{
    using System;

    using Common;
    using Enums;

    public class InternalServerErrorResponse : ViewResponse
    {
        public InternalServerErrorResponse(Exception ex, bool fullStackTrace = false)
            :base(HttpStatusCode.InternalServerError, new InternalServerErrorView(ex))
        {

        }
    }
}
