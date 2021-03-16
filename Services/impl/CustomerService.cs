using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Dto;
using Entities.Models;
using Repository;

namespace Services.impl
{
    public class CustomerService: ICustomerService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CustomerService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        
        public async Task AddCustomer(Customer customer)
        {
            customer.Date = DateTime.Now;
             _repositoryManager.Customer.AddCustomer(customer);
             await _repositoryManager.SaveAsync();

        }

        public async Task<Customer> UpdateCustomer(int customerId, Customer customer)
        {
            customer.Id = customerId;
          _repositoryManager.Customer.UpdateCustomer(customer);
          await _repositoryManager.SaveAsync();
          return customer;
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            var customers = await _repositoryManager.Customer.GetCustomersWithAllOrdersAndProductsAsync();
            var customerDto = customers
                .Select(customer => new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name, 
                    Address = customer.Address, 
                    OrderCount = customer.OrdersCount, 
                    OrderedCost = customer.OrderedCost,
                    Date = customer.Date
                }).ToList();
            return customerDto;
        }

        public async Task<Customer> GetCustomer(int id)
        {
           return await _repositoryManager.Customer.GetCustomerAsync(id);
        }
        
        public async Task DeleteCustomer(int id)
        {
            _repositoryManager.Customer.DeleteCustomer(await _repositoryManager.Customer.GetCustomerAsync(id));
            await _repositoryManager.SaveAsync();
        }
    }
}