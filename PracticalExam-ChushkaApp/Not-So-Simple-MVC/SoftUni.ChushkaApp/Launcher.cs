namespace SoftUni.ChushkaApp
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using WebServer.Data;
    using WebServer.Models;
    using WebServer.Mvc;
    using WebServer.Mvc.Routers;
    using WebServer.Server;

    public class Launcher
    {
        public static void Main()
        {
            var db = new ChushkaDbContext();
            using (db)
            {
                Console.WriteLine("Initialize/Update database...");
                db.Database.Migrate();

                if (!db.Roles.Any())
                {
                    Console.WriteLine("Seeding roles...");

                    db.Roles.Add(new Role() { Name = "Admin" });
                    db.Roles.Add(new Role() { Name = "User" });

                    db.SaveChanges();
                }
                if (!db.ProductTypes.Any())
                {
                    Console.WriteLine("Seeding ProductTypes...");

                    db.ProductTypes.Add(new ProductType() { Name = "Food" });
                    db.ProductTypes.Add(new ProductType() { Name = "Domestic" });
                    db.ProductTypes.Add(new ProductType() { Name = "Health" });
                    db.ProductTypes.Add(new ProductType() { Name = "Cosmetic" });
                    db.ProductTypes.Add(new ProductType() { Name = "Other" });

                    db.SaveChanges();
                }
                Console.WriteLine("Database ready!");
            }
            var server = new WebServer(42420, new ControllerRouter(), new ResourceRouter());

            MvcEngine.Run(server);
        }
    }
}
