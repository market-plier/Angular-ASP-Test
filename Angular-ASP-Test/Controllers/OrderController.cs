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

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderDto>>> GetOrders()
        {
            return await _orderService.GetOrders();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderForUpdateDto>> GetOrder(int id)
        {
            return await _orderService.GetOrder(id);
        }
        [HttpGet("get-order-id")]
        public async Task<ActionResult<int>> GetLastOrderId()
        {
            return await _orderService.GetLastOrderId();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, [FromBody] GetOrderForUpdateDto getOrderForUpdateDto)
        {
            try
            {
                await _orderService.UpdateOrder(id,
                    getOrderForUpdateDto);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, e.Message);
            }
          
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] GetOrderForUpdateDto getOrderForUpdateDto)
        {
            try
            {
              await _orderService.AddOrder(getOrderForUpdateDto.CustomerId,
                    getOrderForUpdateDto.Status,
                    getOrderForUpdateDto.Comment,
                    getOrderForUpdateDto.ProductsDto);
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