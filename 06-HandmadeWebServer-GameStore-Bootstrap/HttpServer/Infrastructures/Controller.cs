namespace HttpServer.Infrastructures
{
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.ComponentModel.DataAnnotations;

    using Server.Http.Contracts;
    using Server.Http.Response;
    using Server.Enums;
    using Server.Http;
    using GameStoreApplication.Services.Contracts;
    using GameStoreApplication.Services;

    public abstract class Controller
    {
        public const string DefaultPath = @"../../../{0}\Resources\{1}.html";
        public const string ContentPlaceholder = "{{{content}}}";
        
        private readonly IUserService userService;

        public Controller(IHttpRequest req)
        {
            this.Request = req;
            this.userService = new UserService();

            this.Authentication = new Authentication(false, false);

            this.ViewData = new Dictionary<string, string>() { ["showError"] = "none" };
            this.ApplyAuthentication();
        }

        protected Authentication Authentication { get; private set; }

        protected abstract string ApplicationDirectory { get; }

        protected IHttpRequest Request { get; set; }

        protected IDictionary<string, string> ViewData { get; private set; }

        protected IHttpResponse RedirectResponse(string route)
        {
            return new RedirectResponse(route);
        }

        protected void ShowError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }

        protected bool ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            
            if (Validator.TryValidateObject(model, context, results, true) == false)
            {
                foreach (var result in results)
                {
                    if (result != ValidationResult.Success)
                    {
                        this.ShowError(result.ErrorMessage);
                        return false;
                    }
                }
            }

            return true;
        }

        protected IHttpResponse FileViewResponse(string fileName)
        {
            var result = this.ProcessFileHtml(fileName);

            if(this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        protected void ApplyAuthentication()
        {
            var anonymousDisplay = "flex";
            var authenticatedDisplay = "none";
            var adminDisplay = "none";

            var isAthenticated = this.Request
                .Session
                .Contains(SessionStore.CurrentUserKey);

            if (!isAthenticated)
            {
                authenticatedDisplay = "none";
                anonymousDisplay = "flex";
            }
            else
            {
                authenticatedDisplay = "flex";
                anonymousDisplay = "none";

                var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
                var isAdmin = this.userService.IsAdmin(email);
                
                if (isAdmin)
                {
                    adminDisplay = "flex";
                }

                this.Authentication = new Authentication(true, isAdmin);
            }

            this.ViewData[nameof(anonymousDisplay)] = anonymousDisplay;
            this.ViewData[nameof(authenticatedDisplay)] = authenticatedDisplay;
            this.ViewData[nameof(adminDisplay)] = adminDisplay;
        }
        
        private string ProcessFileHtml(string fileName)
        {
            var layout = File.ReadAllText(string.Format(DefaultPath, this.ApplicationDirectory, "layout"));

            var fileHtml = File.ReadAllText(string.Format(DefaultPath, this.ApplicationDirectory, fileName));

            var result = layout.Replace(ContentPlaceholder, fileHtml);

            return result;
        }
    }
}
