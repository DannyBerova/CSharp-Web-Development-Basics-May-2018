namespace MyWebServer.Server.Http
{
    using Common;
    using Contracts;
    using Enums;
    using Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class HttpRequest : IHttpRequest
    {
        private readonly string requestText;

        public HttpRequest(string requestText)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestText, nameof(requestText));

            this.requestText = requestText;
            this.FormData = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.UrlParameters = new Dictionary<string, string>();
            this.HeaderCollection = new HttpHeaderCollection();

            this.ParseRequest(requestText);
        }

        public IDictionary<string, string> FormData { get; private set; }

        public HttpHeaderCollection HeaderCollection { get; private set; }

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; }

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestString)
        {
            string[] requestLines = requestString
                .Split(Environment.NewLine);

            if (!requestLines.Any())
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            string[] requestLine = requestLines.First()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3
                || requestLine[2].ToLower() != "http/1.1")
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine.First()); //without ToLower() as in lab.
            this.Url = requestLine[1];
            this.Path = this.ParsePath(this.Url);
            this.ParseHeaders(requestLines);
            this.ParseParameters();
            this.ParseFormData(requestLines.Last());
        }

        private string ParsePath(string url)
            => url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];


        private HttpRequestMethod ParseRequestMethod(string method)
        {
            HttpRequestMethod parsedMethod;

            if (!Enum.TryParse(method, true, out parsedMethod))
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            return parsedMethod;
        }

        private void ParseHeaders(string[] requestLines)
        {
            int endIndex = Array.IndexOf(requestLines, string.Empty);
            for (int i = 1; i < endIndex; i++)
            {
                string[] headersArgs = requestLines[i]
                    .Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);

                if (headersArgs.Length != 2)
                {
                    BadRequestException.ThrowFromInvalidRequest();
                }

                var headerKey = headersArgs[0];
                var headerValue = headersArgs[1].Trim();

                var header = new HttpHeader(headerKey, headerValue);
                this.HeaderCollection.Add(header);
            }

            if (!this.HeaderCollection.ContainsKey(HttpHeader.Host))
            {
                BadRequestException.ThrowFromInvalidRequest();
            }
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains('?'))
            {
                return;
            }

            var query = this.Url.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries).Last();
            this.ParseQuery(query, this.UrlParameters);
        }

        private void ParseFormData(string formDataLine)
        {
            if (this.RequestMethod == HttpRequestMethod.Get)
            {
                return;
            }

            if (formDataLine == "name=")
            {
                formDataLine = "name=1";
            }
            this.ParseQuery(formDataLine, this.FormData);
        }

        private void ParseQuery(string query, IDictionary<string, string> dict)
        {
            if (!query.Contains("="))
            {
                return;
            }

            string[] queryPairs = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var queryPair in queryPairs)
            {
                string[] queryArgs = queryPair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (queryArgs.Length != 2)
                {
                    return;
                }

                var queryKey = WebUtility.UrlDecode(queryArgs[0]);
                var queryValue = WebUtility.UrlDecode(queryArgs[1]);

                dict.Add(queryKey, queryValue);
            }
        }

        public override string ToString() => this.requestText;
    }
}
