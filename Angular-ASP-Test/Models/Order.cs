using System.Collections.Generic;

namespace Angular_ASP_Test.Models
{
    public class Order
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Status { get; set; }
        public ICollection<Product> Products { get; set; }
        public List<ProductOrders> ProductOrders { get; set; }
    }
}