using System.Collections.Generic;
using System.Threading.Tasks;
using Angular_ASP_Test.Data;
using Angular_ASP_Test.Dto;
using Angular_ASP_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Angular_ASP_Test.Services.impl
{
    public class ProductService :IProductService
    {
        private readonly OrderDbContext _context;
        
        public ProductService(OrderDbContext context)
        {
            _context = context;
        }

        public async Task UpdateProduct(int productId, Product product)
        {
            product.Id = productId;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}