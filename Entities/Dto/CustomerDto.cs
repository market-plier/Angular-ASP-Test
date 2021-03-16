using System;

namespace Entities.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal OrderedCost { get; set; }
        public int OrderCount { get; set; }
        public DateTime Date { get; set; }
    }
}