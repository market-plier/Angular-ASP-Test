
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Services.impl
{
    public class ProductService :IProductService
    {
        
        private readonly IRepositoryManager _repositoryManager;

        public ProductService(IRepositoryManager repositoryManager)
        {
         
            _repositoryManager = repositoryManager;
        }
        public async Task UpdateProduct(int productId, Product product)
        {
            
            _repositoryManager.Product.UpdateProduct(productId, product);
            await _repositoryManager.SaveAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _repositoryManager.Product.GetProductsAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _repositoryManager.Product.GetProductAsync(id);
        }

        public async Task DeleteProduct(int product)
        {
            _repositoryManager.Product.DeleteProduct(await _repositoryManager.Product.GetProductAsync(product));
            await _repositoryManager.SaveAsync();
        }

        public async Task AddProduct(Product product)
        {
            product.Date = DateTime.Now;
            _repositoryManager.Product.AddProduct(product);
            await _repositoryManager.SaveAsync();
        }

        public async Task<int> GetLastOrderId()
        {
            return await _repositoryManager.Product.GetLastProductIdAsync();
        }
    }
}