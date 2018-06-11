namespace HttpServer.ByTheCakeApplication.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System;

    using Server.Http.Contracts;
    using Infrastructures;
    using Data;
    using Models;
    using HttpServer.Server.Http.Response;
    using HttpServer.Server.Http;
    using Microsoft.EntityFrameworkCore;

    public class CakesController : BaseController
    {
        private readonly CakesData cakesData;

        public CakesController(IHttpRequest req)
            :base(req)
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse Add()
        {
            this.ViewData["showCart"] = "none";
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Add(IHttpRequest req)
        {
            var name = req.FormData["name"];
            var price = req.FormData["price"];
            var imageUrl = req.FormData["imageUrl"];

            var cake = new Product()
            {
                Name = name,
                Price = decimal.Parse(price),
                ImageUrl = imageUrl
            };

            using (var context = new ByTheCakeContext())
            {
                context.Products.Add(cake);
                context.SaveChanges();
            }

            this.ViewData["name"] = name;
            this.ViewData["price"] = price;
            this.ViewData["imageUrl"] = imageUrl;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            var urlParameters = req.UrlParameters;

            var results = string.Empty;

            this.ViewData["showCart"] = "none";
            this.ViewData["showResult"] = "none";
            this.ViewData["searchTerm"] = string.Empty;

            if (urlParameters.ContainsKey("searchTerm"))
            {
                var searchTerm = urlParameters["searchTerm"];

                this.ViewData["searchTerm"] = searchTerm;

                List<string> cakeResults = null;
                using (var context = new ByTheCakeContext())
                {
                    cakeResults = context.Products
                        .Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()))
                        .Select(c => $"<div><a href=\"/cakes/details/{c.Id}\">{c.Name}</a> - ${c.Price:F2} <a href=\"/shopping/add/{c.Id}?searchTerm={searchTerm}\">Order</a></div>")
                        .ToList();
                }
                   
                results = "No cakes found!";
                
                if(cakeResults.Any())
                {
                    results = string.Join(Environment.NewLine, cakeResults);
                }

                this.ViewData["results"] = results;
            }

            using (var context = new ByTheCakeContext())
            {
                var id = req.Session.Get<int>(SessionStore.CurrentUserKey);
                var user = context.Users.Include(u => u.Orders).FirstOrDefault(u => u.Id == id);

                var orders = user.Orders;

                if (orders.Any())
                {
                    var totalProducts = orders.Count;
                    var totalProductsText = totalProducts != 1 ? "products" : "product";

                    this.ViewData["showCart"] = "block";
                    this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
                }
            }

            if (!string.IsNullOrEmpty(results))
            {
                this.ViewData["showResult"] = "block";
            }

            return this.FileViewResponse(@"cakes\search");
        }

        public IHttpResponse CakeDetails(IHttpRequest req)
        {
            int id = int.Parse(req.UrlParameters["id"]);

            Product cake = null;
            using (var context = new ByTheCakeContext())
            {
                cake = context.Products.Find(id);
            }

            if(cake == null)
            {
                return new BadRequestResponse();
            }

            this.ViewData["name"] = cake.Name;
            this.ViewData["price"] = cake.Price.ToString("F2");
            this.ViewData["imageUrl"] = cake.ImageUrl;

            return this.FileViewResponse(@"cakes\details");

        }

        public IHttpResponse OrderDetails(IHttpRequest req)
        {
            int orderId = int.Parse(req.UrlParameters["id"]);

            using (var context = new ByTheCakeContext())
            {
                const string productLine =
                    "<tr>" +
                        "<td><a href=\"/cakes/details/{0}\">{1}</a></td>" +
                        "<td>${2:F2}</td>" +
                    "</tr>";

                var products = context
                    .ProductOrders
                    .Where(po => po.OrderId == orderId)
                    .Select(p => string.Format(
                        productLine,
                        p.Product.Id,
                        p.Product.Name,
                        p.Product.Price
                    ))
                    .ToList();

                this.ViewData["orderId"] = orderId.ToString();
                this.ViewData["products"] = string.Join("", products);
            }
            return this.FileViewResponse(@"cakes\orderDetails");
        }

        // GET
        public IHttpResponse Orders(IHttpRequest req)
        {
            const string table = "<table class=\"table\" border=\"1\">" +
                                    "<thead class=\"thead-dark\">" +
                                        "<tr>" +
                                            "<th>Order Id</th>" +
                                            "<th>Created On</th>" +
                                            "<th>Sum</th>" +
                                        "</tr>" +
                                        "{0}" +
                                    "</thead>" +
                                "</table>";

            const string orderLine = 
                "<tr>" +
                    "<td><a href=\"cakes/orderDatails/{0}\">{0}</a></td>" +
                    "<td>{1}</td>" +
                    "<td>${2:F2}</td>" +
                "</tr>";
            
            int id = req.Session.Get<int>(SessionStore.CurrentUserKey);

            using (var context = new ByTheCakeContext())
            {
                var orderLines = context
                    .Orders
                    .Where(o => o.UserId == id)                    
                    .Select(o => string.Format(orderLine,
                        o.Id,
                        o.DateOfCreation.ToShortDateString(),
                        o.Products.Sum(p => p.Product.Price)
                    ))
                    .ToList();

                if(orderLines.Count == 0)
                {
                    this.ViewData["orders"] = "<h3>No orders found.</h3>";
                }
                else
                {
                    this.ViewData["orders"] = string.Format(table, string.Join("", orderLines));
                }
            }
            
            return this.FileViewResponse("cakes/orders");
        }
    }
}
