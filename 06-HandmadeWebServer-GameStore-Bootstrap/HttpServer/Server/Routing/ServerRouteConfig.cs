namespace HttpServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using Contracts;
    using Enums;

    public class ServerRouteConfig : IServerRouteConfig
    {
        private IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> routes;

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.AnonymousPaths = new List<string>(appRouteConfig.AnonymousPaths);
            this.AllowedFolders = new List<string>(appRouteConfig.AllowedFolders);

            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();

            var availableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var method in availableMethods)
            {
                this.routes[method] = new Dictionary<string, IRoutingContext>();
            }

            this.InitializeRouteConfig(appRouteConfig);
        }

        public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes => this.routes;

        public ICollection<string> AnonymousPaths { get; private set; }

        public ICollection<string> AllowedFolders { get; private set; }

        private void InitializeRouteConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (var registeredRoute in appRouteConfig.Routes)
            {
                var requestMethod = registeredRoute.Key;
                var routesWithHandler = registeredRoute.Value;

                foreach (var routeWithHandler in routesWithHandler)
                {
                    var route = routeWithHandler.Key;
                    var handler = routeWithHandler.Value;

                    var parameters = new List<string>();

                    var parsedRouteRegex = this.ParseRoute(route, parameters);

                    var routingContext = new RoutingContext(handler, parameters);

                    this.routes[requestMethod].Add(parsedRouteRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string route, List<string> parameters)
        {
            StringBuilder result = new StringBuilder();
            result.Append("^");

            if (route == "/")
            {
                result.Append("/$");
                return result.ToString();
            }

            var tokens = route.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            result.Append("/");

            this.ParseTokens(tokens, parameters, result);

            return result.ToString();
        }

        private void ParseTokens(string[] tokens, List<string> parameters, StringBuilder result)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                string end = i == tokens.Length - 1 ? "$" : "/";

                var currentToken = tokens[i];
                if (!currentToken.StartsWith("{") && !currentToken.EndsWith("}"))
                {
                    result.Append($"{currentToken}{end}");
                    continue;
                }

                var pattern = "<\\w+>";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(currentToken);

                if (!match.Success)
                {
                    throw new InvalidOperationException($"Route parameter in '{currentToken}' is not valid.");
                }

                var parameter = match.Groups[0].Value;
                var parameterName = parameter.Substring(1, parameter.Length - 2);

                parameters.Add(parameterName);

                var currentTokenWithoutCurlyBrackets = currentToken.Substring(1, currentToken.Length - 2);
                result.Append($"{currentTokenWithoutCurlyBrackets}{end}");
            }
        }
    }
}
