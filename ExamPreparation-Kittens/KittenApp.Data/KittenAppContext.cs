
namespace KittenApp.Data
{
    using KittenApp.Models;
    using Microsoft.EntityFrameworkCore;

    public class KittenAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Kitten> Kittens { get; set; }

        public DbSet<Breed> Breeds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"Server=DESKTOP-32FEOB6\SQLEXPRESS;Database=KittenAppYordan;Integrated Security=True";
                optionsBuilder.UseSqlServer(connectionString);

            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

           
            base.OnModelCreating(builder);
        }
    }
}
