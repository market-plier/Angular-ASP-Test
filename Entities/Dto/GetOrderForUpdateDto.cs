using System;
using System.Collections.Generic;

namespace Entities.Dto
{
    public class GetOrderForUpdateDto
    {
        public List<ProductsDto> ProductsDto { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}