namespace SoftUni.WebServer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;
    using WebServer.Models;

    public class OrdersService
    {
        private readonly ChushkaDbContext db;

        public OrdersService()
        {
            this.db = new ChushkaDbContext();
        }

        public bool Create(int productId, int userId, decimal price)
        {
            try
            {
                var order = new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = productId,
                    UserId = userId,
                    SalePrice = price,
                    OrderedOn = DateTime.UtcNow
                };

                this.db.Orders.Add(order);
                this.db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<OrdersListingModel> All()
            => this.db
                .Orders
                .OrderByDescending(o => o.OrderedOn)
                .Select(OrdersListingModel.FromOrder)
                .ToList();
    }
}


       
