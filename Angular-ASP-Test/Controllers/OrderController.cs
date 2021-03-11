using System;
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
        public async Task<ActionResult<IEnumerable<GetOrderDto>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(x => x.Customer)
                .ThenInclude(x => x.Orders)
                .ThenInclude(x => x.ProductOrders)
                .ThenInclude(x=>x.Product)
                .ToListAsync();

            return orders
                .Select(order => new GetOrderDto 
                    {
                        Customer = new CustomerForOrderDto
                        {
                            Name = order.Customer.Name, 
                            Address = order.Customer.Address
                        },
                        Id = order.Id, 
                        Status = order.Status, 
                        OrderedCost = order.ProductOrders
                            .Sum(productOrder => 
                                productOrder.Quantity * productOrder.Product.Price)}).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOrdersDto>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(x=> x.Customer)
                .Include(x=> x.ProductOrders)
                .FirstAsync(x=>x.Id==id);

            if (order == null)
            {
                return NotFound();
            }

            return new ProductOrdersDto
            {
                Comment = order.Comment,
                CustomerId = order.CustomerId,
                Status = order.Status,
                ProductsDto = order.ProductOrders
                    .Select(productOrder => 
                        new ProductsDto
                        {
                            ProductId = productOrder.ProductId, 
                            Quantity = productOrder.Quantity
                        }).ToList()
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, [FromBody] ProductOrdersDto productOrdersDto)
        {
            await _orderService.UpdateOrder(id,
                productOrdersDto);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] ProductOrdersDto productOrdersDto)
        {
            try
            {
              await _orderService.AddOrder(productOrdersDto.CustomerId,
                    productOrdersDto.Status,
                    productOrdersDto.Comment,
                    productOrdersDto.ProductsDto);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return StatusCode(400,e.Message);
            }
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