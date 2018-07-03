namespace SoftUni.WebServer.Services.Models
{
    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using WebServer.Models;

    public class OrdersListingModel
    {
        public string Id { get; set; }

        public string Customer { get; set; }

        public string Product { get; set; }

        public decimal SalePrice { get; set; }

        public string OrderedOn { get; set; }

        public static Expression<Func<Order, OrdersListingModel>> FromOrder =>
            k => new OrdersListingModel()
            {
                Id = k.Id,
                Customer = k.User.Username,
                Product = k.Product.Name,
                SalePrice = k.SalePrice,
                OrderedOn = k.OrderedOn.ToString("hh:mm dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

    }
}
