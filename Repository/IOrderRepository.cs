using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Data;
using Entities.Models;

namespace Repository
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrdersAsync();

        public Task<Order> GetOrderAsync(int id);
        public void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        public Task<List<Order>> GetOrdersWithProductOrders();
        public Task<Order> GetOrderWithProductOrders(int orderId);
        public Task<List<Order>> GetOrdersWithProductOrdersAndProducts();
    }
}