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

        public Task<Customer> AddCustomer()
        {
            throw new System.NotImplementedException();
        }

        public Task<Customer> UpdateCustomer(int orderId, ProductOrdersDto productOrdersDto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            var customers = await _repositoryManager.Customer.GetCustomersAsync();
            var customerDto = customers
                .Select(customer => new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name, 
                    Address = customer.Address, 
                    OrderCount = customer.OrdersCount, 
                    OrderedCost = customer.OrderedCost
                }).ToList();
            return customerDto;
        }

        public async Task<Customer> GetCustomer(int id)
        {
           return await _repositoryManager.Customer.GetCustomerAsync(id);
        }
        
        public async Task DeleteCustomer(int id)
        {
            _repositoryManager.Customer.Delete(await _repositoryManager.Customer.GetCustomerAsync(id));
            await _repositoryManager.SaveAsync();
        }
    }
}