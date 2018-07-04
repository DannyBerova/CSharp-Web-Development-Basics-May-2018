using System;

namespace SoftUni.WebServer.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using WebServer.Models;

    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public static Expression<Func<Product, ProductModel>> FromProduct =>
            p => new ProductModel()
            {
                Name = p.Name,
                TypeId = p.Type.Id,
                TypeName = p.Type.Name,
                Price = p.Price,
                Description = p.Description
            };

    }
}
