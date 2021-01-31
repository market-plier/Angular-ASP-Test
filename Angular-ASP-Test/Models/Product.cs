using System.Collections.Generic;

namespace Angular_ASP_Test.Models
{
    public class Product
    {
        public Product()
        {
            ProductOrdersCollection = new HashSet<ProductOrders>();
        }
        public enum Category
        {
            Products,Drinks,Deserts
        }
        public enum Size
        {
            Big,Medium,Small
        }
        public int Id { get; set; }
        public string Name { get; set; }
       
        public Category ProductCategory { get; set; }
        public Size ProductSize { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<ProductOrders> ProductOrdersCollection { get; set; }
    }
}