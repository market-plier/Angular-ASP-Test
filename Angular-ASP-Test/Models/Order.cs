using System.Collections.Generic;

namespace Angular_ASP_Test.Models
{
    public class Order
    {
        public Order()
        {
            this.ProductOrdersCollection = new HashSet<ProductOrders>();
        }
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string Status { get; set; }
        public virtual ICollection<ProductOrders> ProductOrdersCollection { get; set; }
    }
}