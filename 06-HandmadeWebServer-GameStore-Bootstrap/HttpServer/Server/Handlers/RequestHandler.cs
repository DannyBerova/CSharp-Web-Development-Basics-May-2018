namespace HttpServer.Server.Handlers
{
    using System;

    using Common;
    using Contracts;
    using Http;
    using Http.Contracts;

    public class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));

            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            string sessionIdToSend = null;

            if (!httpContext.Request.Cookies.ContainsKey(SessionStore.SessionCookieKey))
            {
                var sessionId = Guid.NewGuid().ToString();

                httpContext.Request.Session = SessionStore.Get(sessionId);

                sessionIdToSend = sessionId;
            }

            var response = this.handlingFunc.Invoke(httpContext.Request);

            if (sessionIdToSend != null)
            {
                response.Headers.Add(HttpHeader.SetCookie, $"{SessionStore.SessionCookieKey}={sessionIdToSend}; HttpOnly; path=/");
            }

            if (!response.Headers.ContainsKey(HttpHeader.ContentType))
            {
                response.Headers.Add(HttpHeader.ContentType, "text/html");
            }
            
            foreach (var cookie in response.Cookies)
            {
                response.Headers.Add(HttpHeader.SetCookie, cookie.ToString());
            }

            return response;
        }
    }
}
