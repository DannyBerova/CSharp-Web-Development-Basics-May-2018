namespace SimpleMVC.Framework.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Interfaces;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using ActionResults;
    using Attributes.Property;
    using Models;
    using Security;
    using Views;
    using WebServer.Http;
    using WebServer.Http.Contracts;

    public abstract class Controller
    {
        protected Controller()
        {
            this.Model = new ViewModel();
            this.User = new Authentication();
        }

        protected ViewModel Model { get; }

        protected internal IHttpRequest Request { get; internal set; }

        protected internal Authentication User { get; private set; }

        protected IViewable View([CallerMemberName] string caller = "")
        {
            this.InitializeViewModelData();

            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            string fullyQualifiedName = string.Format(
                "../../../{0}/{1}/{2}",
                MvcContext.Get.ViewsFolder,
                controllerName,
                caller);

            IRenderable view = new View(fullyQualifiedName, this.Model.Data);

            return new ViewResult(view);
        }

        protected IRedirectable RedirectToAction(string redirectUrl)
        {
            return new RedirectResult(redirectUrl);
        }

        protected bool IsValidModel(object bindingModel)
        {
            foreach (var property in bindingModel.GetType().GetProperties())
            {
                IEnumerable<Attribute> attributes = property
                    .GetCustomAttributes()
                    .Where(a => a is PropertyAttribute);

                foreach (PropertyAttribute attribute in attributes)
                {
                    if (!attribute.IsValid(property.GetValue(bindingModel)))
                    {
                        return false;
                    }
                }

                IEnumerable<Attribute> validationAttributes = property
                    .GetCustomAttributes()
                    .Where(a => a is ValidationAttribute);

                foreach (ValidationAttribute validationAttribute in validationAttributes)
                {
                    if (!validationAttribute.IsValid(property.GetValue(bindingModel)))
                    {
                        //this.ShowAlert(validationAttribute.FormatErrorMessage(property.Name), "danger");
                        return false;
                    }
                }
            }

            return true;
        }


        protected internal void InitializeController()
        {
            var user = this.Request
                .Session
                .Get<string>(SessionStore.CurrentUserKey);

            if (user != null)
            {
                this.User = new Authentication(user);
            }
        }

        protected void SignIn(string name)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, name);
        }

        protected void SignOut()
        {
            this.Request.Session.Clear();
        }

        private void InitializeViewModelData()
        {
            this.Model["displayType"] = this.User.IsAuthenticated ? "flex" : "none";
            this.Model["authenticatedDisplay"] = this.User.IsAuthenticated ? "none" : "flex";
        }
    }
}