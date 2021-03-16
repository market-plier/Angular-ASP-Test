
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Services
{
    public interface IProductService
    {
        Task UpdateProduct(int productId, Product product);
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task DeleteProduct(int product);
        Task AddProduct(Product product);
        Task<int> GetLastOrderId();
    }
}