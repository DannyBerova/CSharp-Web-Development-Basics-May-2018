namespace SimpleMVC.Framework.Routers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attributes.Methods;
    using Common;
    using Controllers;
    using Interfaces;
    using WebServer.Contracts;
    using WebServer.Enums;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class ControllerRouter : IHandleable
    {
        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;
        private string requestMethod;
        private string controllerName;
        private string actionName;
        private object[] methodParams;

        public IHttpResponse Handle(IHttpRequest request)
        {
            GetRequestData(request);

            GetControllerAndActionName(request);

            MethodInfo action = this.GetMethod();
            if (action == null)
            {
                return new NotFoundResponse();
            }

            this.RetrieveActionParameters(action);

            return PrepareResponse(action);
        }

        private void GetRequestData(IHttpRequest request)
        {
            this.getParams = request.UrlParameters;
            this.postParams = request.FormData;
            this.requestMethod = request.Method.ToString();
        }

        private IHttpResponse PrepareResponse(MethodInfo method)
        {
            var actionResult = (IInvocable)method.Invoke(this.GetController(), this.methodParams);

            var content = actionResult.Invoke();

            IHttpResponse response = new ContentResponse(HttpStatusCode.Ok, content);

            return response;
        }

        private void GetControllerAndActionName(IHttpRequest request)
        {
            string[] invocationParams = request.Path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (invocationParams.Length != 2)
            {
                throw new InvalidOperationException("Invalid URL");
            }

            this.controllerName = invocationParams[0].CapitalizeFirstLetter() + MvcContext.Get.ControllersSuffix;
            this.actionName = invocationParams[1].CapitalizeFirstLetter();
        }

        private MethodInfo GetMethod()
        {
            MethodInfo method = null;

            foreach (var methodInfo in this.GetSuitableMethods())
            {
                IEnumerable<Attribute> attributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>();

                if (!attributes.Any() && this.requestMethod.ToUpper() == "GET")
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(this.requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }
            return method;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            var controller = this.GetController();

            if (controller == null)
            {
                return  new MethodInfo[0];
            }

            return
                this.GetController()
                    .GetType()
                    .GetMethods()
                    .Where(mi => mi.Name == this.actionName);
        }

        private Controller GetController()
        {
            var controllerTypeName = string.Format(
                "{0}.{1}.{2}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                this.controllerName);

            var controllerType = Type.GetType(controllerTypeName);
            if (this.controllerName == null)
            {
                return null;
            }

            var controller = (Controller) Activator.CreateInstance(controllerType);

            return controller;
        }

        private void RetrieveActionParameters(MethodInfo method)
        {
            IEnumerable<ParameterInfo> parameterDescription = method.GetParameters();
            this.methodParams = new object[parameterDescription.Count()];

            int index = 0;
            foreach (var param in parameterDescription)
            {
                if (param.ParameterType.IsPrimitive || param.ParameterType == typeof(string))
                {
                    //GET request
                    object value = this.getParams[param.Name];
                    this.methodParams[index] = Convert
                        .ChangeType(value, param.ParameterType);
                    index++;
                }
                else
                {
                    //POST request
                    Type bindingModelType = param.ParameterType;
                    var modelInstance = Activator.CreateInstance(bindingModelType);
                    var properties = bindingModelType.GetProperties();

                    foreach (var property in properties)
                    {
                        var value = this.postParams[property.Name];
                        property.SetValue(modelInstance, Convert.ChangeType(value, property.PropertyType));
                    }

                    this.methodParams[index] = Convert.ChangeType(
                        modelInstance,
                        bindingModelType);
                    index++;

                }
            }
        }
    }
}
