using System.Collections.Generic;

namespace Angular_ASP_Test.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string ProductCategory { get; set; }
        public string ProductSize { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public List<ProductOrders> ProductOrders{ get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}