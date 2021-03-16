using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string ProductCategory { get; set; }
        public string ProductSize { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public List<ProductOrders> ProductOrders{ get; set; }
        public ICollection<Order> Orders { get; set; }

        public Product()
        {
            ProductOrders = new List<ProductOrders>();
            Orders = new Collection<Order>();
        }
    }
}