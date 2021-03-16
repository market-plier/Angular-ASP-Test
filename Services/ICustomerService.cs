using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Dto;
using Entities.Models;

namespace Services
{
    public interface ICustomerService
    {
        Task AddCustomer(Customer customer);
        Task<Customer> UpdateCustomer(int orderId, Customer customer);
        Task<List<CustomerDto>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task DeleteCustomer(int id);
    }
}