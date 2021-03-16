
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dto;
using Entities.Models;

namespace Services
{
    public interface IOrderService
    {
        Task<int> GetLastOrderId();
        Task<Order> AddOrder(int customerId, string status,string comment, IEnumerable<ProductsDto> productsDto);
        Task<Order> UpdateOrder(int orderId, GetOrderForUpdateDto getOrderForUpdateDto);
        Task<List<GetOrderDto>> GetOrders();
        Task<GetOrderForUpdateDto> GetOrder(int id);
        Task DeleteOrder(int id);
    }
}