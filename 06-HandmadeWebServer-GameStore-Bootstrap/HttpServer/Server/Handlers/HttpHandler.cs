namespace HttpServer.Server.Handlers
{
    using System;
    using System.Text.RegularExpressions;

    using Contracts;
    using Common;
    using Http;
    using Http.Contracts;
    using Http.Response;
    using Routing.Contracts;
    using System.Linq;

    class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;
        
        public HttpHandler(IServerRouteConfig routeConfig)
        {
            CoreValidator.ThrowIfNull(routeConfig, nameof(routeConfig));

            this.serverRouteConfig = routeConfig;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            try
            {
                var anonymousAccessibleRoutes = this.serverRouteConfig.AnonymousPaths;
                var allowedFolders = this.serverRouteConfig.AllowedFolders;

                if(allowedFolders.Any(folder => context.Request.Path.StartsWith(folder)))
                {
                    return new FileResponse(context.Request.Path);
                }
                
                //Check if user is authenticated
                if (!anonymousAccessibleRoutes.Any(
                        ap => new Regex("^"+ap+"$")
                            .Match(context.Request.Path)
                            .Success) && 
                    (context.Request.Session == null || 
                    !context.Request.Session.Contains(SessionStore.CurrentUserKey)))
                {
                    return new RedirectResponse(anonymousAccessibleRoutes.First());
                }

                var requestMethod = context.Request.Method;
                var requestPath = context.Request.Path;
                var registeredRoutes = this.serverRouteConfig.Routes[requestMethod];

                foreach (var registeredRoute in registeredRoutes)
                {
                    var routePattern = registeredRoute.Key;
                    var routingContext = registeredRoute.Value;

                    var routeRegex = new Regex(routePattern);
                    var match = routeRegex.Match(requestPath);

                    if (!match.Success)
                    {
                        continue;
                    }

                    var parameters = routingContext.Parameters;

                    foreach (string parameter in parameters)
                    {
                        var parameterValue = match.Groups[parameter].Value;
                        context.Request.AddUrlParameters(parameter, parameterValue);
                    }

                    return routingContext.Handler.Handle(context);
                }
            }
            catch (Exception ex)
            {
                return new InternalServerErrorResponse(ex);
            }

            return new NotFoundResponse();
        }
    }
}
