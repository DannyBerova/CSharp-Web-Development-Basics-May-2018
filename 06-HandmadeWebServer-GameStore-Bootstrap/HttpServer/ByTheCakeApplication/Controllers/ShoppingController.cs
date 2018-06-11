namespace HttpServer.ByTheCakeApplication.Controllers
{
    using System;
    using System.Linq;

    using Data;
    using Models;
    using Infrastructures;
    using Server.Http;
    using Server.Http.Response;
    using Server.Http.Contracts;

    public class ShoppingController : BaseController
    {
        private readonly CakesData cakesData;

        public ShoppingController(IHttpRequest req)
            :base(req)
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            if (!req.UrlParameters.ContainsKey("id"))
            {
                return new NotFoundResponse();
            }

            var id = int.Parse(req.UrlParameters["id"]);

            using (var context = new ByTheCakeContext())
            {
                Product cake = context.Products.Find(id);
                        
                if(cake == null)
                {
                    return new NotFoundResponse();
                }
                           
                var userId = req.Session.Get<int>(SessionStore.CurrentUserKey);

                var user = context.Users.Find(userId);

                var order = new Order()
                {
                    User = user,
                    DateOfCreation = DateTime.UtcNow
                };

                var productOrder = new ProductOrder()
                {
                    Order = order,
                    Product = cake
                };
                
                order.Products.Add(productOrder);
                cake.Orders.Add(productOrder);
                user.Orders.Add(order);

                context.SaveChanges();
            }

            var redirectUrl = "/search";

            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            int userId = req.Session.Get<int>(SessionStore.CurrentUserKey);

            using(var context = new ByTheCakeContext())
            {
                var orders = context
                    .Orders
                    .Where(o => o.UserId == userId)
                    .ToList();

                if (!orders.Any())
                {
                    this.ViewData["cartItems"] = "No items in your cart.";
                    this.ViewData["totalCost"] = "0.00";
                }
                else
                {
                    var products = context.ProductOrders
                        .Where(po => orders.Any(o => o.Id == po.OrderId))
                        .Select(p => new { p.Product.Name, p.Product.Price})
                        .ToList();

                    var htmlString = products
                        .Select(po => $"<div>{po.Name} - ${po.Price:F2}</div>");

                    var totalPrice = products.Sum(p => p.Price);

                    this.ViewData["cartItems"] = string.Join(string.Empty, htmlString);
                    this.ViewData["totalCost"] = totalPrice.ToString("F2");
                }
            }
            
            return this.FileViewResponse(@"shopping\cart");
        }

        internal IHttpResponse FinishOrder(IHttpRequest req)
        {
            using (var context = new ByTheCakeContext())
            {
                var userId = req.Session.Get<int>(SessionStore.CurrentUserKey);

                var orders = context.Orders
                    .Where(o => o.UserId == userId);

                var productOrders = context.ProductOrders
                    .Where(po => orders.Any(o => o.Id == po.OrderId));
                    
                context.Orders.RemoveRange(orders);
                context.ProductOrders.RemoveRange(productOrders);
                context.SaveChanges();
            }

            return this.FileViewResponse(@"shopping\finish-order");
        }
    }
}
