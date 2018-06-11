namespace HttpServer.Server.Http.Response
{
    using Contracts;
    using HttpServer.Server.Common;
    using HttpServer.Server.Enums;

    class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl)
            :base()
        {
            CoreValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));

            this.StatusCode = HttpStatusCode.Found;
            this.Headers.Add(new HttpHeader("Location", redirectUrl));
        }
    }
}
