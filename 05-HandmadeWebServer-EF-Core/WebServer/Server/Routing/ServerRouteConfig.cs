namespace MyWebServer.Server.Routing
{
    using Contracts;
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class ServerRouteConfig : IServerRouteConfig
    {
        private readonly IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> routes;

        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>>();

            var availableMethods = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (var requestMethod in availableMethods)
            {
                this.routes[requestMethod] = new Dictionary<string, IRoutingContext>();
            }

            InitializeServerConfig(appRouteConfig);
        }

        public IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes => this.routes;

        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {

            foreach (var kvp in appRouteConfig.Routes)
            {
                var requestMethod = kvp.Key;
                var routesWithHandlers = kvp.Value;
                foreach (var requestHandler in routesWithHandlers)
                {
                    var route = requestHandler.Key;
                    var handler = requestHandler.Value;

                    var parameters = new List<string>();

                    var parsedRegex = ParseRoute(route, parameters);

                    var routingContext = new RoutingContext(handler, parameters);

                    this.routes[requestMethod].Add(parsedRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string route, List<string> parameters)
        {
            var parsedRegex = new StringBuilder();


            if (route == "/")
            {
                parsedRegex.Append("/$");
                return parsedRegex.ToString();
            }

            parsedRegex.Append("^/");


            var tokens = route.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            ParseTokens(tokens, parameters, parsedRegex);

            return parsedRegex.ToString();
        }

        private void ParseTokens(string[] tokens, List<string> parameters, StringBuilder parsedRegex)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                var end = i == tokens.Length - 1 ? "$" : "/";
                var currentToken = tokens[i];

                if (!currentToken.StartsWith('{') && !currentToken.EndsWith('}'))
                {
                    parsedRegex.Append($"{currentToken}{end}");
                    continue;
                }

                var pattern = "<\\w+>";
                var parameterRegex = new Regex(pattern);
                var parameterMatch = parameterRegex.Match(currentToken);

                if (!parameterMatch.Success)
                {
                    throw new InvalidOperationException($"Route parameter in '{currentToken}' is not valid.");
                }

                var match = parameterMatch.Value;
                var parameter = match.Substring(1, match.Length - 2);

                //if (parameters.Contains(parameter))
                //{
                //    parameters.Add(parameter.Trim('$') + "/$");
                //}
                parameters.Add(parameter);

                var currentTokenWithoutCurlyBrackets = currentToken.Substring(1, currentToken.Length - 2);
                parsedRegex.Append($"{currentTokenWithoutCurlyBrackets}{end}");
            }
        }
    }
}
