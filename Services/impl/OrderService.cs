using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Data;
using Entities.Dto;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Services.impl
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repositoryManager;

        public OrderService(OrderDbContext context, IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task<List<GetOrderDto>> GetOrders()
        {
            var orders = await _repositoryManager.Order.GetOrdersWithProductOrdersAndProducts();


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
                            productOrder.Quantity * productOrder.Product.Price)
                }).ToList();
        }

        public async Task<GetOrderForUpdateDto> GetOrder(int id)
        {
            try
            {
                var order = await _repositoryManager.Order.GetOrderWithProductOrders(id);
                order.Customer = await _repositoryManager.Customer.GetCustomerAsync(order.CustomerId);

                return new GetOrderForUpdateDto
                {
                    Date = order.Date,
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
            catch (InvalidOperationException e)
            {
                throw new ArgumentException("Can't get order");
            }
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _repositoryManager.Order.GetOrderAsync(id);
            _repositoryManager.Order.DeleteOrder(order);
            await _repositoryManager.SaveAsync();
        }

        public async Task<int> GetLastOrderId()
        {
            return await _repositoryManager.Order.GetLastOrderIdAsync();
        }

        public async Task<Order> AddOrder(int customerId, string status, string comment,
            IEnumerable<ProductsDto> productsDto)
        {
            var order = new Order {Status = status, Comment = comment, CustomerId = customerId, Date = DateTime.Now};
            _repositoryManager.Order.CreateOrder(order);
            foreach (var ordersDto in productsDto)
            {
                var product = await _repositoryManager.Product.GetProductAsync(ordersDto.ProductId);
                if (product.Quantity < ordersDto.Quantity)
                {
                    throw new ArgumentException("Can't order more than available");
                }

                product.ProductOrders.Add(new ProductOrders() {Quantity = ordersDto.Quantity, Order = order});
                product.Quantity -= ordersDto.Quantity;
            }

            await _repositoryManager.SaveAsync();
            return order;
        }

        public async Task<Order> UpdateOrder(int orderId, GetOrderForUpdateDto getOrderForUpdateDto)
        {
            var order = await _repositoryManager.Order.GetOrderWithProductOrders(orderId);
            order.CustomerId = getOrderForUpdateDto.CustomerId;
            order.Comment = getOrderForUpdateDto.Comment;
            order.Status = getOrderForUpdateDto.Status;
            foreach (var productOrder in order.ProductOrders)
            {
                var product = await _repositoryManager.Product.GetProductAsync(productOrder.ProductId);
                product.Quantity += productOrder.Quantity;
                product.ProductOrders.Remove(productOrder);
            }
            await _repositoryManager.SaveAsync();

            foreach (var ordersDto in getOrderForUpdateDto.ProductsDto)
            {
                var product = await _repositoryManager.Product.GetProductAsync(ordersDto.ProductId);
                if (product.Quantity < ordersDto.Quantity)
                {
                    throw new ArgumentException("Available only " + product.Quantity + " of " + product.Name);
                }
                product.Quantity -= ordersDto.Quantity;
                product.ProductOrders.Add(new ProductOrders
                {
                    Order = order,
                    Quantity = ordersDto.Quantity
                });
            }

            await _repositoryManager.SaveAsync();
            return order;
        }
    }
}