using Angular_ASP_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Angular_ASP_Test.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrders> ProductOrders { get; set; }
    }
}