using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dto;
using Entities.Models;

namespace Services
{
    public interface ICustomerService
    {
        Task<Customer> AddCustomer();
        Task<Customer> UpdateCustomer(int orderId, ProductOrdersDto productOrdersDto);
        Task<List<CustomerDto>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task DeleteCustomer(int id);
    }
}