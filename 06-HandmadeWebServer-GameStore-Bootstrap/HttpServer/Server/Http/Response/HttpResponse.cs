﻿namespace HttpServer.Server.Http.Response
{
    using System.Text;

    using Contracts;
    using Enums;

    public abstract class HttpResponse : IHttpResponse
    {
        private string StatusCodeMessage => this.StatusCode.ToString();

        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
        }
        
        public IHttpHeaderCollection Headers { get; protected set; }

        public IHttpCookieCollection Cookies { get; protected set; }

        public HttpStatusCode StatusCode { get; protected set; }

        public override string ToString()
        {
            var response = new StringBuilder();

            var statusCodeNumber = (int)this.StatusCode;

            response.AppendLine($"HTTP/1.1 {statusCodeNumber} {this.StatusCodeMessage}");

            response.AppendLine(this.Headers.ToString());
            
            return response.ToString();
        }
    }
}
