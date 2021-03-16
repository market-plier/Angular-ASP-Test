using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.impl
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        
        public ProductRepository(OrderDbContext orderDbContext)
            : base(orderDbContext)
        {
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await FindByCondition(product => product.Id == id).SingleAsync();
        }

        public void UpdateProduct(int id, Product product)
        {
            product.Id = id;
            Update(product);
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public void AddProduct(Product product)
        {
            Create(product);
        }

        public async Task<int> GetLastProductIdAsync()
        {
            return await _orderDbContext.Products.MaxAsync(product => product.Id);
        }
    }
}