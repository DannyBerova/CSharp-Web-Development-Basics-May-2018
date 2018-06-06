namespace MyWebServer.ByTheCakeApp.Controllers
{
    using Data;
    using Infrastructure;
    using Models;
    using Server.Http.Contracts;
    using System;
    using System.Linq;

    public class CakesController : Controller
    {
        private readonly CakesData cakesData;

        public CakesController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse Add()
        {
            this.ViewData["name"] = "Please, enter data in both fields:";
            this.ViewData["price"] = "[name] and [price].";
            this.ViewData["showResult"] = "block";
        
            return FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Add(IHttpRequest req)
        {
            var formNameKey = "name";
            var formPriceKey = "price";
            
            if (!req.FormData.ContainsKey(formNameKey) ||
                !req.FormData.ContainsKey(formPriceKey))
            {
                this.ViewData["name"] = "Please, enter data in both fields:";
                this.ViewData["price"] = "[name] and [price].";
                this.ViewData["showResult"] = "block";

                return FileViewResponse(@"cakes\add");
            }
            else
            {
                var name = req.FormData[formNameKey];
                var price = req.FormData[formPriceKey];

                var cake = new Cake
                {
                    Name = name,
                    Price = decimal.Parse(price)
                };

                this.cakesData.Add(name, price);

                var priceToShow = decimal.Parse(price);

                this.ViewData["name"] = $"name: {name}";
                this.ViewData["price"] = $"price: {priceToShow:F2}";
                this.ViewData["showResult"] = "block";
            }

            return FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string searchTermKey = "searchTerm";

            var urlParameters = req.UrlParameters;

            this.ViewData["results"] = string.Empty;
            this.ViewData["searchTerm"] = string.Empty;

            if (urlParameters.ContainsKey(searchTermKey))
            {
                var searchTerm = urlParameters[searchTermKey];

                this.ViewData["searchTerm"] = searchTerm;

                var savedCakesDivs = this.cakesData
                    .All()
                    .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Select(c => $@"<div>{c.Name} - ${c.Price:F2} <a href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div>");

                var results = "No cakes found";

                if (savedCakesDivs.Any())
                {
                    results = string.Join(Environment.NewLine, savedCakesDivs);
                }

                this.ViewData["results"] = results;
            }
            else
            {
                this.ViewData["results"] = "Please, enter search term";
            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.Orders.Any())
            {
                var totalProducts = shoppingCart.Orders.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
            }

            return FileViewResponse(@"cakes\search");
        }
    }
}
