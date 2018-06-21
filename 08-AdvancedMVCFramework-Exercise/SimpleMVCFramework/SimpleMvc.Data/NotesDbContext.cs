namespace SimpleMvc.Data
{
    using Constants;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class NotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

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

            builder.Entity<User>(entity =>
            {
                entity
                    .HasMany(u => u.Notes)
                    .WithOne(n => n.User)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(builder);
        }
    }
}
