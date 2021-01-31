using System.Collections;
using System.Collections.Generic;

namespace Angular_ASP_Test.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal OrderedCost { get; set; }
        public int OrdersCount { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}