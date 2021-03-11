namespace Angular_ASP_Test.Dto
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public CustomerForOrderDto Customer { get; set; }
        public decimal OrderedCost { get; set; }
        
    }
}