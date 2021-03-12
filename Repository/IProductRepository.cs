using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Repository
{
    public interface IProductRepository
    {
        public  Task<List<Product>> GetProductsAsync();
        public  Task<Product> GetProductAsync(int id);
        void Update(Product product);
    }
}