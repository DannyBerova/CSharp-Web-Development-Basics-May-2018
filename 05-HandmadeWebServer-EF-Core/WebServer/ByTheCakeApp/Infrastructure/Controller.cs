namespace MyWebServer.ByTheCakeApp.Infrastructure
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Views.Home;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;

    public abstract class Controller
    {
        public const string DefaultPath = @"..\..\..\ByTheCakeApp\Resources\{0}.html";

        public const string ContentPlaceholder = "{{{content}}}";

        protected Controller()
        {
            this.ViewData = new Dictionary<string, string>
            {
                ["authDisplay"] = "block",
                ["showError"] = "none"
            };
        }

        protected IDictionary<string, string> ViewData { get; private set; }

        protected IHttpResponse FileViewResponse(string fileName)
        {
            var result = ProcessFileHtml(fileName);

            if (this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        private static string ProcessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));

            var fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));

            var result = layoutHtml.Replace(ContentPlaceholder, fileHtml);
            return result;
        }

        protected void AddError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }

    }
}
