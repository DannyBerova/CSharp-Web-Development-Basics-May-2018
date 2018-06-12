namespace SimpleMVC.Framework.ViewEngine.Generic
{
    using System;
    using Interfaces.Generic;
    public class ActionResult<T> : IActionResult<T>
    {

        public ActionResult(string viewFullyQualifiedName, T model)
        {
            this.Action = (IRenderable<T>)Activator
                .CreateInstance(Type.GetType(viewFullyQualifiedName));

            this.Action.Model = model;
        }

        public IRenderable<T> Action { get; set; }

        public string Invoke()
        {
            return this.Action.Render();
        }
    }
}
