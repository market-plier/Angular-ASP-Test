using Angular_ASP_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Angular_ASP_Test.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Order>()
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity<ProductOrders>(
                    j =>
                        j
                            .HasOne(po => po.Product)
                            .WithMany(p => p.ProductOrders)
                            .HasForeignKey(po => po.ProductId)
                    ,
                    j =>
                        j
                            .HasOne(po => po.Order)
                            .WithMany(o => o.ProductOrders)
                            .HasForeignKey(po => po.OrderId)
                    ,
                    j =>
                    {
                        j.Property(pt => pt.Quantity).HasDefaultValue(1);
                        j.HasKey(t => new {t.OrderId, t.ProductId});
                    }
                );
        }
    }
}