using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Data;
using Entities.Dto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Angular_ASP_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(OrderDbContext context, IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderDto>>> GetOrders()
        {
            return await _orderService.GetOrders();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOrdersDto>> GetOrder(int id)
        {
            return await _orderService.GetOrder(id);
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
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}