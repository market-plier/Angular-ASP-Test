using System.Collections.Generic;
using Angular_ASP_Test.Models;

namespace Angular_ASP_Test.Dto
{
    public class ProductOrdersDto
    {
        public List<ProductsDto> ProductsDto { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}