using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Repository
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        Task<List<Customer>> GetCustomersAsync();
        Task<List<Customer>> GetCustomersWithAllOrdersAndProductsAsync();
        Task<Customer> GetCustomerAsync(int id);
        void UpdateCustomer(Customer product);
        void DeleteCustomer(Customer product);
    }
}