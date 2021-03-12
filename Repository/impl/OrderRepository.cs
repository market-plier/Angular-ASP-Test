using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.impl
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext orderDbContext)
            : base(orderDbContext)
        {
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            return await FindByCondition(order => order.Id == id).SingleAsync();
        }

        public void CreateOrder(Order order)
        {
            Create(order);
        }

        public void UpdateOrder(Order order)
        {
            Update(order);
        }

        public void DeleteOrder(Order order)
        {
            Delete(order);
        }

        public async Task<List<Order>> GetOrdersWithProductOrders()
        {
            return await FindAll().Include(order => order.ProductOrders).ToListAsync();
        }

        public async Task<Order> GetOrderWithProductOrders(int orderId)
        {
            return await FindByCondition(order => order.Id == orderId).Include(order => order.ProductOrders).SingleAsync();
        }

        public async Task<List<Order>> GetOrdersWithProductOrdersAndProducts()
        {
            return await FindAll()
                .Include(x => x.Customer)
                .Include(x => x.ProductOrders)
                .ThenInclude(x=>x.Product)
                .ToListAsync();
        }
    }
}