using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Order
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Status { get; set; }
        
        public string Comment { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public ICollection<Product> Products { get; set; }
        public List<ProductOrders> ProductOrders { get; set; }

        public Order()
        {
            Products = new Collection<Product>();
            ProductOrders = new List<ProductOrders>();
        }
    }
}