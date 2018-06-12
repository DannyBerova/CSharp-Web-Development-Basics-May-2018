namespace SimpleMVC.Framework.Controllers
{
    using Interfaces;
    using Interfaces.Generic;
    using ViewEngine;
    using ViewEngine.Generic;
    using System.Runtime.CompilerServices;

    public abstract class Controller
    {
        protected IActionResult View([CallerMemberName]string caller = "")
        {
            string controllerName = this.GetControllerName();

            var fullyQualifiedName = this.GetFullyQualifiedName(controllerName, caller);

            return new ActionResult(fullyQualifiedName);
        }

        protected IActionResult View(string controller, string action)
        {
            var fullyQualifiedName = this.GetFullyQualifiedName(controller, action);

            return new ActionResult(fullyQualifiedName);
        }

        protected IActionResult<TModel> View<TModel>(TModel model, [CallerMemberName]string caller = "")
        {
            var controllerName = this.GetControllerName();

            var fullyQualifiedName = this.GetFullyQualifiedName(controllerName, caller);

            return new ActionResult<TModel>(fullyQualifiedName, model);
        }

        protected IActionResult<TModel> View<TModel>(TModel model, string controller, string action)
        {
            var fullyQualifiedName = this.GetFullyQualifiedName(controller, action);

            return new ActionResult<TModel>(fullyQualifiedName, model);
        }

        private string GetControllerName()
            => this.GetType()
                .Name
                .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

        private string GetFullyQualifiedName(string controllerName, string action)
            => string.Format("{0}.{1}.{2}.{3}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controllerName,
                action);
    }
}