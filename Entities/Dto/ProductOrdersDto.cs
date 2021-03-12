using System.Collections.Generic;

namespace Entities.Dto
{
    public class ProductOrdersDto
    {
        public List<ProductsDto> ProductsDto { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}