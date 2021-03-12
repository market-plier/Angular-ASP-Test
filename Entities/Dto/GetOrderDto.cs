namespace Entities.Dto
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public CustomerForOrderDto Customer { get; set; }
        public decimal OrderedCost { get; set; }
        
    }
}