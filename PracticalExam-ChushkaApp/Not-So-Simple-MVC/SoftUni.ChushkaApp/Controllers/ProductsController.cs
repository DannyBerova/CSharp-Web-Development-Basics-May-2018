namespace SoftUni.ChushkaApp.Controllers
{
    using System;
    using System.Linq;
    using Infrastructure;
    using Models;
    using WebServer.Models;
    using WebServer.Mvc.Attributes.HttpMethods;
    using WebServer.Mvc.Interfaces;
    using WebServer.Services;

    public class ProductsController : BaseController
    {
        private readonly ProductsService products;

        public ProductsController()
        {
            this.products = new ProductsService();
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (!this.User.IsAuthenticated || !this.User.Roles.Contains("Admin"))
            {
                return RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Add(ProductCreateBindingModel model)
        {
            if (!this.User.Roles.Contains("Admin"))
            {
                return RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                SetValidatorErrors();
                return this.View();
            }

            ProductType productType = this.products.GetProductType(model.Type);

            if (productType == null)
            {
                this.ShowError(Constants.InvalidPossitionMessage);
                return this.View();
            }

            var id = this.products.Create(model.Name, model.Price, model.Description, productType);
            if (id == default(int))
            {
                ShowError(Constants.UnsuccessfullOperationMessage);
                return this.View();
            }

            return RedirectToAction($"/products/details?id={id}");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }

            var product = this.products.GetById(id);
            this.ViewData["name"] = product.Name;
            this.ViewData["typeName"] = product.TypeName;
            this.ViewData["price"] = product.Price.ToString();
            this.ViewData["description"] = product.Description;

            this.ViewData["id"] = id.ToString();

            this.ViewData["adminActions"] = String.Empty;

            if (this.User.Roles.Contains("Admin"))
            {
                this.ViewData["adminActions"] = $@"<a class=""btn chushka-bg-color"" href=""/admin/edit?id={id}"">Edit</a>
                                                <a class=""btn chushka-bg-color"" href=""/admin/delete?id={id}"">Delete</a>";
            }
            return this.View();
        }
    }
}
