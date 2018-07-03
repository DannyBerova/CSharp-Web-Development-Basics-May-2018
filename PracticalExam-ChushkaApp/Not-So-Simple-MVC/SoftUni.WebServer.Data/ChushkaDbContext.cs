namespace SoftUni.WebServer.Data
{
    using Constants;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ChushkaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConfigurationString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            //builder.Entity<Order>()
            //    .HasKey(o => new {o.ProductId, o.UserId });

            //builder.Entity<User>(entity =>
            //{
            //    entity
            //        .HasMany(u => u.Products)
            //        .WithOne(t => t.User)
            //        .HasForeignKey(t => t.UserId);
            //});

            //builder.Entity<Product>(entity =>
            //{
            //    entity
            //        .HasMany(u => u.Users)
            //        .WithOne(t => t.Product)
            //        .HasForeignKey(t => t.ProductId);
            //});


            base.OnModelCreating(builder);
        }
    }
}
