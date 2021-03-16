using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);
        void UpdateProduct(int id, Product product);
        void DeleteProduct(Product product);
        void AddProduct(Product product);
        Task<int> GetLastProductIdAsync();
    }
}