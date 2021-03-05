using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular_ASP_Test.Data;
using Angular_ASP_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular_ASP_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly OrderDbContext _context;

        public ProductController(OrderDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
             await _context.Products.AddAsync(product);
             await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    }

}