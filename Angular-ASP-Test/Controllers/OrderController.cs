using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular_ASP_Test.Data;
using Angular_ASP_Test.Dto;
using Angular_ASP_Test.Models;
using Angular_ASP_Test.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular_ASP_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly OrderDbContext _context;
        private readonly IOrderService _orderService;
        public OrderController(OrderDbContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetCOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order product)
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
                if (!OrderExists(id))
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
        public async Task<ActionResult<Order>> PostOrder([FromForm] int customerId,[FromForm] string status,[FromForm] List<ProductOrdersDto> productOrders)
        {
            var order = await _orderService.AddOrder(customerId, status, productOrders);
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

    }
}