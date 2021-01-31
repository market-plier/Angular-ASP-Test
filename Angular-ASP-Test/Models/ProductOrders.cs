using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Angular_ASP_Test.Models
{
    public class ProductOrders
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}