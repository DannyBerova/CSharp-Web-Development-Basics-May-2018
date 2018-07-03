namespace SoftUni.ChushkaApp.Controllers
{
    using System;
    using System.Text;
    using Infrastructure;
    using Models;
    using WebServer.Models;
    using WebServer.Mvc.Attributes.HttpMethods;
    using WebServer.Mvc.Interfaces;
    using WebServer.Services;

    public class AdminController : BaseController
    {
        private readonly UsersService users;
        private readonly ProductsService products;
        private readonly OrdersService orders;

        public AdminController()
        {
            this.users = new UsersService();
            this.products = new ProductsService();
            this.orders = new OrdersService();
        }

        public IActionResult Edit(int id)
        {
            this.ViewData["id"] = id.ToString();
            return this.PrepareEditAndDeleteView(id)
                ?? this.View();
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductCreateBindingModel model)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            if (!this.IsValidModel(model))
            {
                SetValidatorErrors();
                return this.Edit(id);
            }

            ProductType productType = this.products.GetProductType(model.Type);

            if (productType == null)
            {
                this.ShowError(Constants.InvalidPossitionMessage);
                return this.Edit(id);
            }

            this.products.Update(id, model.Name, model.Price, model.Description, productType);

            this.ViewData["id"] = id.ToString();

            return this.RedirectToHome();
        }

        public IActionResult Delete(int id)
        {
            this.ViewData["id"] = id.ToString();

            return this.PrepareEditAndDeleteView(id) ?? this.View();
        }

        [HttpPost]
        public IActionResult Confirm(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var postTitle = this.products.Delete(id);

            return this.RedirectToHome();
        }

        [HttpGet]
        public IActionResult Orders()
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var ordersRows = this.orders.All();

            var builder = new StringBuilder();
            var counter = 1;
            foreach (var order in ordersRows)
            {
                builder.Append($@"
                    <tr>
                        <td>{counter++}</td>
                        <td>{order.Id}</td>
                        <td>{order.Customer}</td>
                        <td>{order.Product}</td>
                        <td>{order.SalePrice}</td>
                        <td>{order.OrderedOn}</td>
                    </tr>");
            }

            this.ViewData["orders"] = builder.ToString();

            return this.View();
        }

        [HttpGet]
        private IActionResult PrepareEditAndDeleteView(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var product = this.products.GetById(id);

            if (product == null)
            {
                return this.RedirectToHome();
            }

            this.ViewData["id"] = id.ToString();

            this.ViewData["name"] = product.Name;
            this.ViewData["typeName"] = product.TypeName;
            this.ViewData["price"] = product.Price.ToString("F2");
            this.ViewData["description"] = product.Description;

            if (product.TypeId != default(int))
            {
               this.ViewData[$"{product.TypeId}Check"] = "checked";
            }

            return null;
        }
    }
}
