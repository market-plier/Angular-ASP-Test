using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.impl
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(OrderDbContext orderDbContext)
            : base(orderDbContext)
        {
        }

        public void AddCustomer(Customer customer)
        {
            Create(customer);
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<List<Customer>> GetCustomersWithAllOrdersAndProductsAsync()
        {
            return await FindAll().Include(x=>x.Orders)
                .ThenInclude(x=>x.ProductOrders)
                .ThenInclude(x=> x.Product)
                .ToListAsync();;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await FindByCondition(customer => customer.Id == id).SingleAsync();
        }

        public void UpdateCustomer(Customer customer)
        {
           Update(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
           Delete(customer);
        }
    }
}