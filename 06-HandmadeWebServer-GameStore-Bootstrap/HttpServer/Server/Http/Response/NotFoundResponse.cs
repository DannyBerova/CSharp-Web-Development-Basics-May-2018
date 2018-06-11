namespace HttpServer.Server.Http.Response
{
    using Common;

    public class NotFoundResponse : ViewResponse
    {
        public NotFoundResponse() 
            : base(Enums.HttpStatusCode.NotFound, new NotFoundView())
        {
            
        }
    }
}
