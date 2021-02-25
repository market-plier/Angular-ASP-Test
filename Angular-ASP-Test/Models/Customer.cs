using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Angular_ASP_Test.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [NotMapped] public decimal OrderedCost
        {

            get
            {
                return Orders.
                    Sum(order => order.ProductOrders.Select
                        (x => x.Product.Price * x.Quantity).Sum());
            }
        }

        [NotMapped]
        public int OrdersCount => Orders.Count;
        public ICollection<Order> Orders { get; set; }
    }
}