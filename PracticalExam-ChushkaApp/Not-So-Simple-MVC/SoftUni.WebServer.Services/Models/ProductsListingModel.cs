namespace SoftUni.WebServer.Services.Models
{
    using System;
    using System.Linq.Expressions;
    using WebServer.Models;

    public class ProductsListingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public static Expression<Func<Product, ProductsListingModel>> FromPost =>
            k => new ProductsListingModel()
            {
                Id = k.Id,
                Name = k.Name,
                Description = k.Description,
                Price = k.Price
            };

    }
}
