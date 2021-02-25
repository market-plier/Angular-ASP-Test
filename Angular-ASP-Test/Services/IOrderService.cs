using System.Collections.Generic;
using System.Threading.Tasks;
using Angular_ASP_Test.Dto;
using Angular_ASP_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace Angular_ASP_Test.Services
{
    public interface IOrderService
    {
        Task<Order> AddOrder(int customerId, string status, IEnumerable<ProductOrdersDto> productOrdersDto);
    }
}