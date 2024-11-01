using Microsoft.EntityFrameworkCore;


namespace ECommerceDbContext.Models
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User entity
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Configure Product entity
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            // Configure Category entity
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            // Configure Order entity
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            // Configure OrderItem entity
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);

            // Define relationships
            modelBuilder.Entity<OrderItem>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(oi => oi.OrderId);

            // Additional configurations can be added here as needed.
        }

    }
}
