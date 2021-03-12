
using System.Threading.Tasks;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Services.impl
{
    public class ProductService :IProductService
    {
        private readonly OrderDbContext _context;
        private readonly IRepositoryManager _repositoryManager;

        public ProductService(OrderDbContext context, IRepositoryManager repositoryManager)
        {
            _context = context;
            _repositoryManager = repositoryManager;
        }

        public async Task UpdateProduct(int productId, Product product)
        {
            product.Id = productId;
            _repositoryManager.Product.Update(product);
            await _repositoryManager.SaveAsync();
        }
    }
}