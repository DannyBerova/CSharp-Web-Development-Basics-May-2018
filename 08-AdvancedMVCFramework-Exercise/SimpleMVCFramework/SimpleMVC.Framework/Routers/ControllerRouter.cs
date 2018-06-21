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
        public ControllerRouter()
        {

        }

        public IHttpResponse Handle(IHttpRequest request)
        {
            IDictionary<string, string> getParams = request.UrlParameters;
            IDictionary<string, string> postParams = request.FormData;
            string requestMethod = request.Method.ToString().ToUpper();

            string[] invocationParams = request.Path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (request.Path == "/")
            {
                invocationParams = new[] {"home", "index"};
            }
            if (invocationParams.Length != 2)
            {
                throw new InvalidOperationException("Invalid URL");
            }

            var controllerName = invocationParams[0].CapitalizeFirstLetter() + MvcContext.Get.ControllersSuffix;
            var actionName = invocationParams[1].CapitalizeFirstLetter();

            Controller controller = this.GetController(controllerName);

            if (controller != null)
            {
                controller.Request = request;
                controller.InitializeController();
            }

            MethodInfo method = this.GetMethod(controller, requestMethod, actionName);

            if (method == null)
            {
                return new NotFoundResponse();
            }

            IEnumerable<ParameterInfo> parameters = method.GetParameters();

            object[] methodParams = this.AddParameters(parameters, getParams, postParams);

            try
            {
                IHttpResponse response = this.GetResponse(method, controller, methodParams);
                return response;
            }
            catch (Exception e)
            {
                return new InternalServerErrorResponse(e);
            }
        }

        private object[] AddParameters(IEnumerable<ParameterInfo> parameters,
            IDictionary<string, string> getParams,
            IDictionary<string, string> postParams)
        {
            object[] methodParams = new object[parameters.Count()];

            int index = 0;

            foreach (ParameterInfo parameter in parameters)
            {
                if (parameter.ParameterType.IsPrimitive ||
                    parameter.ParameterType == typeof(string))
                {
                    methodParams[index] = this.ProcessPrimitiveParameter(parameter, getParams);
                    index++;
                }
                else
                {
                    methodParams[index] = this.ProcessComplexParameter(parameter, postParams);
                    index++;
                }
            }

            return methodParams;
        }

        private object ProcessPrimitiveParameter(ParameterInfo parameter, IDictionary<string, string> getParams)
        {
            object value = getParams[parameter.Name];
            return Convert.ChangeType(value, parameter.ParameterType);
        }

        private object ProcessComplexParameter(ParameterInfo parameter, IDictionary<string, string> postParams)
        {
            Type bindingModelType = parameter.ParameterType;
            object bindingModel = Activator.CreateInstance(bindingModelType);

            IEnumerable<PropertyInfo> properties = bindingModelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                property.SetValue(
                    bindingModel,
                    Convert.ChangeType(postParams[property.Name],
                    property.PropertyType));
            }

            return Convert.ChangeType(bindingModel, bindingModelType);
        }


        private MethodInfo GetMethod(Controller controller, string requestMethod, string actionName)
        {
            MethodInfo method = null;

            foreach (var methodInfo in this.GetSuitableMethods(controller, actionName))
            {
                IEnumerable<Attribute> attributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>();

                if (!attributes.Any() && requestMethod.ToUpper() == "GET")
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }
            return method;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods(Controller controller, string actionName)
        {
            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return
                controller
                    .GetType()
                    .GetMethods()
                    .Where(mi => mi.Name == actionName);
        }

        private Controller GetController(string controllerName)
        {
            var controllerTypeName = string.Format(
                "{0}.{1}.{2}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                controllerName);

            var controllerType = Type.GetType(controllerTypeName);
            if (controllerType == null)
            {
                return null;
            }

            var controller = (Controller)Activator.CreateInstance(controllerType);

            return controller;
        }

        private IHttpResponse GetResponse(MethodInfo method, Controller controller, object[] methodParams)
        {
            object actionResult = method.Invoke(controller, methodParams);

            IHttpResponse response = null;

            if (actionResult is IViewable)
            {
                string content = ((IViewable)actionResult).Invoke();

                response = new ContentResponse(HttpStatusCode.Ok, content);
            }
            else if (actionResult is IRedirectable)
            {
                string redirectUrl = ((IRedirectable)actionResult).Invoke();

                response = new RedirectResponse(redirectUrl);
            }

            return response;
        }
    }
}
