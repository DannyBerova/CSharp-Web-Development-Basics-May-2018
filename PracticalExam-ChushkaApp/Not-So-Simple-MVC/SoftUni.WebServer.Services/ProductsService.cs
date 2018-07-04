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
                var product = new Product
                {
                    Name = name.CapitalizeFirstLetter(),
                    Price = price,
                    Description = description,
                    Type = type
                };

                this.db.Products.Add(product);
                this.db.SaveChanges();

                return product.Id;
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
                .Select(ProductModel.FromProduct)
                .FirstOrDefault();

        public void Update(int id, string name, decimal price, string description, ProductType type)
        {
            var product = this.db.Products.Find(id);

            if (product == null)
            {
                return;
            }

            product.Name = name.CapitalizeFirstLetter();
            product.Price = price;
            product.Description = description;
            product.Type = type;

            this.db.SaveChanges();
        }

        public string Delete(int id)
        {
            var product = this.db.Products.Find(id);

            if (product == null)
            {
                return null;
            }

            this.db.Products.Remove(product);
            this.db.SaveChanges();

            return product.Name;
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

