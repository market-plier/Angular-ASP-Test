
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dto;
using Entities.Models;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> AddOrder(int customerId, string status,string comment, IEnumerable<ProductsDto> productsDto);
        Task<Order> UpdateOrder(int orderId, ProductOrdersDto productOrdersDto);
        Task<List<GetOrderDto>> GetOrders();
        Task<ProductOrdersDto> GetOrder(int id);
        Task DeleteOrder(int id);
    }
}