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

        public async Task<ProductOrdersDto> GetOrder(int id)
        {
            try
            {
                var order = await _repositoryManager.Order.GetOrderWithProductOrders(id);
                order.Customer = await _repositoryManager.Customer.GetCustomerAsync(order.CustomerId);

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

        public async Task<Order> AddOrder(int customerId, string status, string comment,
            IEnumerable<ProductsDto> productsDto)
        {
            var order = new Order {Status = status, Comment = comment, CustomerId = customerId};
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

        public async Task<Order> UpdateOrder(int orderId, ProductOrdersDto productOrdersDto)
        {
            var order = await _repositoryManager.Order.GetOrderWithProductOrders(orderId);
            order.CustomerId = productOrdersDto.CustomerId;
            order.Comment = productOrdersDto.Comment;
            order.Status = productOrdersDto.Status;
            foreach (var productOrder in order.ProductOrders)
            {
                var product = await _repositoryManager.Product.GetProductAsync(productOrder.ProductId);
                product.Quantity += productOrder.Quantity;
                product.ProductOrders.Remove(productOrder);
            }

            foreach (var ordersDto in productOrdersDto.ProductsDto)
            {
                var product = await _repositoryManager.Product.GetProductAsync(ordersDto.ProductId);
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