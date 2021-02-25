using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular_ASP_Test.Data;
using Angular_ASP_Test.Dto;
using Angular_ASP_Test.Models;

namespace Angular_ASP_Test.Services.impl
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;

        public OrderService(OrderDbContext context)
        {
            _context = context;
        }


        public async Task<Order> AddOrder(int customerId, string status, IEnumerable<ProductOrdersDto> productOrdersDto)
        {
            var order = new Order {Status = status};
            await _context.Orders.AddAsync(order);
            var customer = await _context.Customers.FindAsync(customerId);
            customer?.Orders.Add(order);
            await _context.SaveChangesAsync();
            foreach (var ordersDto in productOrdersDto)
            {
                var product = await _context.Products.FindAsync(ordersDto.ProductId);
                product.ProductOrders.Add(new ProductOrders() {Quantity = ordersDto.Quantity, Order = order});
                await _context.SaveChangesAsync();
            }

            return order;
        }
    }
}