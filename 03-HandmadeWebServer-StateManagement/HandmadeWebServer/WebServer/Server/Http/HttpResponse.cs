﻿using MyWebServer.Server.Http.Contracts;

namespace MyWebServer.Server.Http
{
    using Enums;
    using System.Text;

    public abstract class HttpResponse : IHttpResponse
    {
        private string statusCodeMessage => this.StatusCode.ToString();

        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
        }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public HttpStatusCode StatusCode { get; protected set; }

       // public string StatusMessage => this.StatusCode.ToString();

        //public void AddHeader(string location, string redirectUrl)
        //{
        //    this.Headers.Add(new HttpHeader(location, redirectUrl));
        //}

        public override string ToString()
        {
            var response = new StringBuilder();

            var statusCodeNumber = (int)this.StatusCode;

            response.AppendLine($"HTTP/1.1 {statusCodeNumber} {this.statusCodeMessage}");

            response.AppendLine(this.Headers.ToString());
            //response.AppendLine();

            return response.ToString();
        }
    }
}
