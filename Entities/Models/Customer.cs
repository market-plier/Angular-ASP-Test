using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Entities.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        [NotMapped] public decimal OrderedCost
        {
            get
            {
                return Orders?.
                    Sum(order => order.ProductOrders?.Select
                        (x => x.Product.Price * x.Quantity).Sum()) ?? 0;
            }
        }

        [NotMapped]
        public int OrdersCount => Orders?.Count ?? 0;

        public ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new Collection<Order>();
        }
    }
}