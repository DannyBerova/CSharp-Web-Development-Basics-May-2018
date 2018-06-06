namespace MyWebServer.ByTheCakeApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ByTheCakeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        //public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.;Database=ByTheCakeDb;Integrated Security=True;");
            //base.OnConfiguring(builder);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            builder
                .Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            builder
                .Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne(op => op.Order)
                .HasForeignKey(op => op.OrderId);

            builder
                .Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(op => op.Product)
                .HasForeignKey(op => op.ProductId);

        }

    }
}
