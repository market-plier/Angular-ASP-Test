using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Repository
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetCustomersAsync();
        public Task<List<Customer>> GetCustomersWithAllOrdersAndProductsAsync();
        public Task<Customer> GetCustomerAsync(int id);
        void Update(Customer product);
        void Delete(Customer product);
    }
}