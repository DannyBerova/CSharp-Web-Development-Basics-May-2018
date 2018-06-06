namespace MyWebServer.Server.Handlers
{
    using Common;
    using Contracts;
    using Http;
    using Http.Contracts;
    using System;

    public class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));

            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            string sessionIdToSend = null;

            if (!context.Request.Cookies.ContainsKey(SessionStore.sessionCookieKey))
            {
                var sessionId = Guid.NewGuid().ToString();

               context.Request.Session =  SessionStore.Get(sessionId);

                sessionIdToSend = sessionId;
            }
            var response = this.handlingFunc(context.Request);

            if (sessionIdToSend != null)
            {
                response.Headers.Add(
                    HttpHeader.SetCookie,
                    $"{SessionStore.sessionCookieKey}={sessionIdToSend}; HttpOnly; path=/");
            }

            if (!response.Headers.ContainsKey(HttpHeader.ContentType))
            {
                response.Headers.Add(HttpHeader.ContentType, "text/plain");
            }

            foreach (var cookie in response.Cookies)
            {
                response.Headers.Add(HttpHeader.SetCookie, cookie.ToString());
            }

            return response;
        }

        //public IHttpResponse Handle(IHttpContext httpContext)
        //{
        //    IHttpResponse httpResponse = this.handlingFunc(httpContext.Request);

        //    // if(!httpResponse.Headers.ContainsKey(HttpHeader.ContentType, "text/plain"))

        //    //if (!httpResponse.Headers.ContainsKey(HttpHeader.ContentType)
        //    //{

        //    //}
        //    httpResponse.Headers.Add(new HttpHeader("Content-Type", "text/html"));
        //    // httpResponse.AddHeader("Content-Type", "text/html");

        //    return httpResponse;
        //}
    }
}
