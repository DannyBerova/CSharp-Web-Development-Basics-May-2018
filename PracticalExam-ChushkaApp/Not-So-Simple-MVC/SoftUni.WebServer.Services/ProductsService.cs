namespace SoftUni.WebServer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Data;
    using Models;
    using WebServer.Models;

    public class ProductsService
    {
        private readonly ChushkaDbContext db;

        public ProductsService()
        {
            this.db = new ChushkaDbContext();
        }

        public int Create(string name, decimal price, string description, ProductType type)
        {
            try
            {
                var post = new Product
                {
                    Name = name.CapitalizeFirstLetter(),
                    Price = price,
                    Description = description,
                    Type = type
                };

                this.db.Products.Add(post);
                this.db.SaveChanges();

                return post.Id;
            }
            catch
            {
                return default(int);
            }
        }

        public IEnumerable<ProductsListingModel> All()
            => this.db
                .Products
                .Select(ProductsListingModel.FromPost)
                .ToList();

       

        public ProductModel GetById(int id)
            => this.db
                .Products
                .Where(p => p.Id == id)
                .Select(ProductModel.FromPost)
                .FirstOrDefault();

        public void Update(int id, string name, decimal price, string description, ProductType type)
        {
            var post = this.db.Products.Find(id);

            if (post == null)
            {
                return;
            }

            post.Name = name.CapitalizeFirstLetter();
            post.Price = price;
            post.Description = description;
            post.Type = type;

            this.db.SaveChanges();
        }

        public string Delete(int id)
        {
            var post = this.db.Products.Find(id);

            if (post == null)
            {
                return null;
            }

            this.db.Products.Remove(post);
            this.db.SaveChanges();

            return post.Name;
        }

        public ProductType GetProductType(int modelType)
        {
            var productType = this.db.ProductTypes.Find(modelType);
            return productType;
        }

        public ProductType GetProductType(string modelType)
        {
            var productType = this.db.ProductTypes.Find(modelType);
            return productType;
        }

        public decimal GetSalePrice(int id)
        {
            return this.db.Products.Find(id).Price;
        }
    }
}

