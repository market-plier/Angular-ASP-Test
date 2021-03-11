using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Angular_ASP_Test.Data;
using Angular_ASP_Test.Dto;
using Angular_ASP_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Angular_ASP_Test.Services.impl
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;
        
        public OrderService(OrderDbContext context)
        {
            _context = context;
        }


        public async Task<Order> AddOrder(int customerId, string status,string comment, IEnumerable<ProductsDto> productsDto)
        {
            var order = new Order {Status = status,Comment = comment};
            await _context.Orders.AddAsync(order);
            var customer = await _context.Customers.FindAsync(customerId);
            customer.Orders.Add(order);
            foreach (var ordersDto in productsDto)
            {
                var product = await _context.Products.FindAsync(ordersDto.ProductId);
                if (product.Quantity < ordersDto.Quantity)
                {
                    throw new ArgumentException("Can't order more than available");
                }
                product.ProductOrders.Add(new ProductOrders() {Quantity = ordersDto.Quantity, Order = order});
                product.Quantity -= ordersDto.Quantity;
            }
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrder(int orderId, ProductOrdersDto productOrdersDto)
        {
            var order = await _context.Orders.Include(x=>x.ProductOrders).FirstAsync(x=>x.Id==orderId);
            var customer = await _context.Customers.FindAsync(productOrdersDto.CustomerId);
            order.Customer = customer;
            order.Comment = productOrdersDto.Comment;
            order.Status = productOrdersDto.Status;
            foreach (var productOrder in order.ProductOrders)
            {
                var product = await _context.Products.FindAsync(productOrder.ProductId);
                product.Quantity += productOrder.Quantity;
                product.ProductOrders.Remove(productOrder);
            }

            await _context.SaveChangesAsync();
            foreach (var ordersDto in productOrdersDto.ProductsDto)
            {
                var product = await _context.Products.FindAsync(ordersDto.ProductId);
                product.Quantity -= ordersDto.Quantity;
                product.ProductOrders.Add(new ProductOrders
                {
                    Order = order,
                    Quantity = ordersDto.Quantity
                });
            }
            await _context.SaveChangesAsync();
            return null;
        }
    }
}